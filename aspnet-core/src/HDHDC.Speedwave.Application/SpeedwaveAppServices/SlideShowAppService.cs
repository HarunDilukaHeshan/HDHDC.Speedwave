using HDHDC.Speedwave.BlobServices.Managers;
using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class SlideShowAppService : ApplicationService, ISlideShowAppService
    {
        protected ISlideShowPictureManager SlideShowPictureManager { get; }

        public SlideShowAppService(
            ISlideShowPictureManager slideShowPictureManager)
        {
            SlideShowPictureManager = slideShowPictureManager;
        }

        public async Task<SlideShowPictureDto[]> GetArrayAsync()
        {
            return await SlideShowPictureManager.GetArrayAsync();
        }

        public async Task<SlideShowPictureDto> GetAsync(string fileName)
        {
            return await SlideShowPictureManager.GetAsync(fileName);
        }

        public async Task<SlideShowPictureDto> SaveAsync(SlideShowPictureDto dto)
        {
            return await SlideShowPictureManager.SaveXmlAsync(dto);
        }

        public async Task<SlideShowPictureDto> UpdateAsync(string fileName, SlideShowPictureDto dto)
        {
            return await SlideShowPictureManager.UpdateAsync(fileName, dto);
        }

        public async Task DeleteAsync(string fileName)
        {
            await SlideShowPictureManager.DeleteAsync(fileName);
        }
    }
}
