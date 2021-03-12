using HDHDC.Speedwave.BlobServices.Containers;
using HDHDC.Speedwave.BlobServices.Options;
using HDHDC.Speedwave.BlobStoringServices;
using HDHDC.Speedwave.UtilityServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public class ItemPictureManager : IItemPictureManager
    {
        protected ISpeedwaveBlobContainer BlobContainer { get; }
        protected ItemPicturesBlobOptions Options { get; }
        protected IImageManipulator ImageManipulator { get; }
        protected IMd5Hasher Md5Hasher { get; }

        public ItemPictureManager(
            IOptions<ItemPicturesBlobOptions> options,
            ISpeedwaveBlobContainer<ItemPicturesBlobContainer> blobContainer, 
            IImageManipulator imageManipulator, 
            IMd5Hasher md5Hasher)
        {
            BlobContainer = blobContainer;
            Options = options.Value;
            ImageManipulator = imageManipulator;
            Md5Hasher = md5Hasher;
        }

        public async Task<string> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) throw new ArgumentException();
            
            //var buffer = await BlobContainer.GetAllBytesOrNullAsync(name);

            var buffer = await BlobContainer.GetAllBytesOrNullAsync(name) ?? new byte[0];
            var b64 = Convert.ToBase64String(buffer);

            //return Convert.ToBase64String(buffer);
            return (string.IsNullOrWhiteSpace(b64)) ? "" : string.Concat("data:image/jpg;base64,", b64);
        }

        public async Task<IList<string>> GetListAsync(int id)
        {
            var paddedId = id.ToString().PadLeft(5, '0');
            var searchPattern = string.Format("{0}.{1}", paddedId, Options.ImageFormat.ToString());

            var files = await BlobContainer.GetBytesListAsync(searchPattern, 0);
            var base64List = new List<string>();

            foreach (var file in files)
            {
                var b64 = Convert.ToBase64String(file);
                var dataUri = (string.IsNullOrWhiteSpace(b64)) ? "" : string.Concat("data:image/jpg;base64,", b64);
                base64List.Add(dataUri);
            }

            return base64List;
        }

        public async Task<IList<BlobFile>> GetBlobFileListAsync(int id, int skipCount = 0, int maxResultCount = 10)
        {
            var paddedId = id.ToString().PadLeft(5, '0');
            var searchPattern = string.Format("*{0}.{1}", paddedId, Options.ImageFormat.ToString());
            var blobFiles = await BlobContainer.GetListWithFileInfoAsync(searchPattern, skipCount, maxResultCount);

            return blobFiles.ToList();
        }

        public async Task<string> SaveAsync(int id, string base64, bool overrideExisting = false)
        {
            if (string.IsNullOrEmpty(base64) || string.IsNullOrWhiteSpace(base64) || 
                !ImageManipulator.IsImageStream(base64) || id < 1)
                throw new ArgumentException();

            var b64Part = GetBase64Part(base64);
            var buffer = Convert.FromBase64String(b64Part);
            var blobName = await SaveAsync(id, buffer, overrideExisting);

            return blobName;
        }

        public async Task<string> SaveAsync(int id, byte[] bytes, bool overrideExisting = false)
        {
            if (bytes == null || !ImageManipulator.IsImageStream(bytes) || id < 1)
                throw new ArgumentException();

            var buffer = ImageManipulator.ResizeImage(bytes, Options.Width, Options.Height, Options.ImageFormat);

            var name = GenerateName(buffer, true);
            var paddedId = id.ToString().PadLeft(5, '0');

            var blobName = string.Format("{0}{1}{2}.{3}", Options.Prefix, name, paddedId, Options.ImageFormat.ToString());
            await BlobContainer.SaveAsync(blobName, buffer, overrideExisting);
            return blobName;
        }

        public async Task<string> SaveThumbnailAsync(string base64, bool overrideExisting = false)
        {
            if (string.IsNullOrEmpty(base64) || string.IsNullOrWhiteSpace(base64) ||
                !ImageManipulator.IsImageStream(base64))
                throw new ArgumentException();

            var b64Part = GetBase64Part(base64);
            var buffer = Convert.FromBase64String(b64Part);
            return await SaveThumbnailAsync(buffer, overrideExisting);
        }

        public async Task<string> SaveThumbnailAsync(byte[] bytes, bool overrideExisting = false)
        {
            if (bytes == null || !ImageManipulator.IsImageStream(bytes))
                throw new ArgumentException();

            var buffer = ImageManipulator.ResizeImage(bytes, Options.ThumbnailWidth, Options.ThumbnailHeight, Options.ImageFormat);

            var name = GenerateName(buffer);

            var blobName =  string.Format("{0}{1}.{2}", Options.Prefix, name, Options.ImageFormat.ToString());
            await BlobContainer.SaveAsync(blobName, buffer, overrideExisting);
            return blobName;
        }

        public async Task DeleteAsync(string name)
        {
            if (name == null) throw new ArgumentNullException();
            await BlobContainer.DeleteAsync(name);
        }

        private string GetBase64Part(string base64) =>
            (base64.Contains(',') ? base64.Substring(base64.IndexOf(',') + 1) : base64);
        private string GenerateName(string base64, bool randomize = true)
        {
            if (string.IsNullOrEmpty(base64) || string.IsNullOrWhiteSpace(base64) || !ImageManipulator.IsImageStream(base64))
                throw new ArgumentException();

            var buffer = Convert.FromBase64String(base64);

            return GenerateName(buffer, randomize);
        }

        private string GenerateName(byte[] bytes, bool randomize = true)
        {
            if (bytes == null || !ImageManipulator.IsImageStream(bytes))
                throw new ArgumentException();

            var b16Str = Md5Hasher.GenerateBase16Hash(bytes);
            var seededName = b16Str;

            if (randomize)
            {
                var seed = DateTime.Now.Second * DateTime.Now.Minute;
                seededName += seed.ToString("X");
            }

            return seededName;
        }
    }
}
