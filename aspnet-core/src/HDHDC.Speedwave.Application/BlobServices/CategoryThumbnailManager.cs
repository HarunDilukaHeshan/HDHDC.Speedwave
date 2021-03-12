using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.UtilityServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.BlobServices
{    
    public class CategoryThumbnailManager
    {
        protected ISpeedwaveBlobContainer BlobContainer { get; }
        protected IImageManipulator ImageManipulator { get; }
        protected SpeedwaveBlobOptions Options { get; }
        protected IRepository<CategoryEntity, int> CategoryRepository { get; }
        protected IMd5Hasher Md5Hasher { get; }

        public CategoryThumbnailManager(
            ISpeedwaveBlobContainer blobContainer,
            IImageManipulator imageManipulator,
            IOptions<SpeedwaveBlobOptions> options,
            IRepository<CategoryEntity, int> categoryRepository, 
            IMd5Hasher md5Hasher)
        {
            BlobContainer = blobContainer;
            ImageManipulator = imageManipulator;
            CategoryRepository = categoryRepository;
            Options = options.Value;
            Md5Hasher = md5Hasher;
        }

        public async Task SaveAsync(int categoryId, string base64Img)
        {            

            var categoryE = await CategoryRepository.FindAsync(categoryId) ?? throw new Exception();

            if (!ImageManipulator.IsImageStream(base64Img)) throw new Exception();

            var buffer = Convert.FromBase64String(base64Img);

            var resizedImgBuffer = ImageManipulator.ResizeImage(
                buffer,
                Options.CategoryThumbnail.Width,
                Options.CategoryThumbnail.Height,
                Options.CategoryThumbnail.ImageFormat);

            var hash = Md5Hasher.GenerateBase16Hash(resizedImgBuffer);
            var randomizedHash = Randomize(hash);
            var fileName = string.Format("{0}.{1}", randomizedHash, Options.CategoryThumbnail.ImageFormat.ToString());

            await BlobContainer.DeleteAsync(categoryE.CategoryThumbnail);

            await BlobContainer.SaveBytesAsync(fileName, resizedImgBuffer);

            categoryE.CategoryThumbnail = fileName;
            await CategoryRepository.UpdateAsync(categoryE, autoSave: true);
        }

        public async Task<string> SaveAsync(string base64Img)
        {
            if (!ImageManipulator.IsImageStream(base64Img)) throw new Exception();

            var buffer = Convert.FromBase64String(base64Img);

            var resizedImgBuffer = ImageManipulator.ResizeImage(
                buffer,
                Options.CategoryThumbnail.Width,
                Options.CategoryThumbnail.Height,
                Options.CategoryThumbnail.ImageFormat);

            var hash = Md5Hasher.GenerateBase16Hash(resizedImgBuffer);
            var randomizedHash = Randomize(hash);
            var fileName = string.Format("{0}{1}.{2}", Options.CategoryThumbnail.Prefix, randomizedHash, Options.CategoryThumbnail.ImageFormat.ToString());

            fileName = await BlobContainer.SaveBytesAsync(fileName, resizedImgBuffer);

            return fileName;
        }

        public async Task<string> GetAsync(int categoryId)
        {
            var categoryE = await CategoryRepository.FindAsync(categoryId) ?? throw new Exception();
            var buffer = await BlobContainer.GetBytesAsync(categoryE.CategoryThumbnail);
            return Convert.ToBase64String(buffer);
        }

        private string Randomize(string hash)
        {
            var time = DateTime.Now.Second * DateTime.Now.Minute;
            return hash + time.ToString("X");
        }
    }
}
