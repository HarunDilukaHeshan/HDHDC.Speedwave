using HDHDC.Speedwave.BlobServices.Containers;
using HDHDC.Speedwave.BlobServices.Options;
using HDHDC.Speedwave.BlobStoringServices;
using HDHDC.Speedwave.Options;
using HDHDC.Speedwave.UtilityServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Linq;
using Volo.Abp.BlobStoring;
using Volo.Abp.Guids;
using HDHDC.Speedwave.DTOs;
using System.IO;
using System.Drawing.Imaging;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public class SlideShowPictureManager : ISlideShowPictureManager
    {
        protected ISpeedwaveBlobContainer<SlideShowBlobContainer> BlobContainer { get; }
        protected IImageManipulator ImageManipulator { get; }
        protected IMd5Hasher Md5Hasher { get; }
        protected SlideShowBlobOptions Options { get; }
        protected XmlSettings XmlSettings { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public SlideShowPictureManager(
            IGuidGenerator guidGenerator,
            IOptions<SlideShowBlobOptions> options,
            IOptions<XmlSettings> xmlSettings,
            ISpeedwaveBlobContainer<SlideShowBlobContainer> blobContainer,
            IMd5Hasher md5Hasher,
            IImageManipulator imageManipulator)
        {
            BlobContainer = blobContainer;
            ImageManipulator = imageManipulator;
            Md5Hasher = md5Hasher;
            Options = options.Value;
            XmlSettings = xmlSettings.Value;
            GuidGenerator = guidGenerator;
        }

        public async Task<SlideShowPictureDto> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) throw new ArgumentException();

            var buffer = await BlobContainer.GetAllBytesOrNullAsync(name) ?? throw new FileNotFoundException();
            var xmlStr = Encoding.UTF8.GetString(buffer);
            var xmlDoc = CreateXmlDoc();

            xmlDoc.LoadXml(xmlStr);

            var slideShowElm = xmlDoc.DocumentElement;
            var imgElm = slideShowElm.FirstChild;
            var b64txtNode = imgElm.FirstChild as XmlText;
            var imgTypeAttr = imgElm.Attributes.Item(0);

            if (imgTypeAttr.LocalName != "imgType") throw new Exception();

            var dto = new SlideShowPictureDto { 
                Name = slideShowElm.GetAttribute("name"),
                Uri = slideShowElm.GetAttribute("uri"),
                Base64Picture = SetB64ImgContentType(b64txtNode.InnerText, imgTypeAttr.InnerText),
                FileName = name
            };

            return dto;
        }

        public async Task<SlideShowPictureDto[]> GetArrayAsync()
        {
            var blobArr = await BlobContainer.GetListWithFileInfoAsync(Options.Prefix + "*", 0);
            var dtoList = new List<SlideShowPictureDto>();
            var xmlDoc = new XmlDocument();
            var settings = GetReaderSettings();

            foreach (var b in blobArr)
            {
                using var ms = new MemoryStream(b.Blob);
                using var reader = XmlReader.Create(ms, settings);

                xmlDoc.Load(reader);

                var slideShowElm = xmlDoc.DocumentElement;
                var imgElm = slideShowElm.FirstChild;
                var b64txtNode = imgElm.FirstChild as XmlText;
                var imgTypeAttr = imgElm.Attributes.Item(0);

                if (imgTypeAttr.LocalName != "imgType") throw new Exception();

                dtoList.Add(new SlideShowPictureDto
                {
                    Name = slideShowElm.GetAttribute("name"),
                    Uri = slideShowElm.GetAttribute("uri"),
                    Base64Picture = SetB64ImgContentType(b64txtNode.InnerText, imgTypeAttr.InnerText),
                    FileName = b.FileName
                });
            }

            return dtoList.ToArray();
        }

        public async Task<SlideShowPictureDto> UpdateAsync(string fileName, SlideShowPictureDto dto)
        {
            if (string.IsNullOrWhiteSpace(fileName) || 
                !ImageManipulator.IsImageStream(dto.Base64Picture)) throw new ArgumentException();

            var b64Part = (dto.Base64Picture == null)? null : GetBase64Part(dto.Base64Picture);

            var file = await BlobContainer.GetAllBytesOrNullAsync(fileName) ?? throw new FileNotFoundException();
            var xmlDoc = new XmlDocument();
            var settings = GetReaderSettings();

            using var ms = new MemoryStream(file);
            using var reader = XmlReader.Create(ms, settings);

            xmlDoc.Load(reader);

            var slideShowElm = xmlDoc.DocumentElement;
            var imgElm = slideShowElm.FirstChild;
            var b64txtNode = imgElm.FirstChild as XmlText;

            b64txtNode.InnerText = b64Part ?? b64txtNode.InnerText;
            slideShowElm.SetAttribute("uri", (dto.Uri == null) ? slideShowElm.GetAttribute("uri") : dto.Uri.ToString());
            slideShowElm.SetAttribute("name", dto.Name ?? slideShowElm.GetAttribute("name"));

            var bytes = Encoding.UTF8.GetBytes(xmlDoc.InnerXml);
            await BlobContainer.SaveAsync(fileName, bytes, true);
            return await GetAsync(fileName);
        }

        public async Task DeleteAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException();

            await BlobContainer.DeleteAsync(fileName);
        }

        public async Task<SlideShowPictureDto> SaveXmlAsync(SlideShowPictureDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Base64Picture) || !ImageManipulator.IsImageStream(dto.Base64Picture)) 
                throw new ArgumentException();
            var base64Part = GetBase64Part(dto.Base64Picture);
            var buffer = ImageManipulator.ResizeImage(base64Part, Options.Width, Options.Height, Options.ImageFormat);

            try
            {
                var xmlDoc = CreateXmlDoc();                
                var slideShowElm = xmlDoc.DocumentElement;
                var imgElm = slideShowElm.FirstChild;
                var imgTypeAttr = xmlDoc.CreateAttribute("imgType");

                imgTypeAttr.Value = Options.ImageFormat.ToString();
                slideShowElm.SetAttribute("name", dto.Name);
                slideShowElm.SetAttribute("uri", (dto.Uri == null)? "" : dto.Uri.ToString());

                imgElm.Attributes.Append(imgTypeAttr);
                imgElm.AppendChild(xmlDoc.CreateTextNode(buffer));
      
                var name  = await SaveAsync(xmlDoc.OuterXml);
                return await GetAsync(name);
            }
            //catch(XmlException ex)
            //{

            //}
            //catch(XmlSchemaValidationException ex)
            //{

            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private XmlDocument CreateXmlDoc()
        {
            var reader = XmlReader.Create(XmlSettings.SlideShowTemplateUri.ToString(), GetReaderSettings());
            var xmlDoc = new XmlDocument();
            
            xmlDoc.Load(reader);

            xmlDoc.Validate((obj, args) =>
            {
                throw args.Exception;
            });

            reader.Dispose();

            return xmlDoc;
        }

        private XmlReaderSettings GetReaderSettings()
        {
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(null, XmlSettings.SlideShowSchemaUri.ToString());
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (obj, args) =>
            {
                throw args.Exception;
            };

            return settings;
        }

        protected async Task<string> SaveAsync(string xmlStr, bool overrideExisting = false)
        {
            if (string.IsNullOrWhiteSpace(xmlStr)) throw new ArgumentException();            
            var buffer = Encoding.UTF8.GetBytes(xmlStr);

            return await SaveAsync(buffer, overrideExisting);
        }

        protected async Task<string> SaveAsync(byte[] bytes, bool overrideExisting = false)
        {
            if (bytes == null) throw new ArgumentNullException();

            var name = GenerateName(bytes);

            var blobName = string.Format("{0}{1}.{2}", Options.Prefix, name, Options.ImageFormat.ToString());
            await BlobContainer.SaveAsync(blobName, bytes, overrideExisting);
            return blobName;
        }

        private string GenerateName(byte[] bytes, bool randomize = true)
        {
            if (bytes == null) throw new ArgumentException();

            var b16Str = Md5Hasher.GenerateBase16Hash(bytes);
            var seededName = b16Str;

            if (randomize)
            {
                var seed = DateTime.Now.Second * DateTime.Now.Minute;
                seededName += seed.ToString("X");
            }

            return seededName;
        }

        private string GetBase64Part(string base64) =>
            (base64.Contains(',') ? base64.Substring(base64.IndexOf(',') + 1) : base64);

        private string SetB64ImgContentType(string base64Str, string imageFormat) 
            => string.Concat("data:image/" + imageFormat + ";base64,", base64Str);        
    }
}
