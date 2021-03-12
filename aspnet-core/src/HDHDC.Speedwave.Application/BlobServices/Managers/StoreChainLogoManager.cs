using HDHDC.Speedwave.BlobServices.Containers;
using HDHDC.Speedwave.BlobServices.Options;
using HDHDC.Speedwave.BlobStoringServices;
using HDHDC.Speedwave.UtilityServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public class StoreChainLogoManager : IStoreChainLogoManager
    {
        protected ISpeedwaveBlobContainer<StoreChainLogoBlobContainer> BlobContainer { get; }
        protected StoreChainLogoBlobOptions Options { get; }
        protected IImageManipulator ImageManipulator { get; }
        protected IMd5Hasher Md5Hasher { get; }

        public StoreChainLogoManager(
           IOptions<StoreChainLogoBlobOptions> options,
           ISpeedwaveBlobContainer<StoreChainLogoBlobContainer> blobContainer,
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

            var buffer = await BlobContainer.GetAllBytesOrNullAsync(name) ?? new byte[0];
            var b64 = Convert.ToBase64String(buffer);

            return (string.IsNullOrWhiteSpace(b64)) ? "" : string.Concat("data:image/jpg;base64,", b64);
        }

        public async Task<string> SaveAsync(string base64, bool overrideExisting = false)
        {
            if (string.IsNullOrEmpty(base64) || string.IsNullOrWhiteSpace(base64) ||
                !ImageManipulator.IsImageStream(base64))
                throw new ArgumentException();

            var b64Part = GetBase64Part(base64);
            var buffer = Convert.FromBase64String(b64Part);
            return await SaveAsync(buffer, overrideExisting);
        }

        public async Task<string> SaveAsync(byte[] bytes, bool overrideExisting = false)
        {
            if (bytes == null || !ImageManipulator.IsImageStream(bytes))
                throw new ArgumentException();

            var buffer = ImageManipulator.ResizeImage(bytes, Options.Width, Options.Height, Options.ImageFormat);

            var name = GenerateName(buffer);

            var blobName = string.Format("{0}{1}.{2}", Options.Prefix, name, Options.ImageFormat.ToString());
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
