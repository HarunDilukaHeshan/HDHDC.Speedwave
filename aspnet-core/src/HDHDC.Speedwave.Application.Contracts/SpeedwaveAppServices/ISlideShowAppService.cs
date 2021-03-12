using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface ISlideShowAppService : IApplicationService
    {

        Task<SlideShowPictureDto[]> GetArrayAsync();
        Task<SlideShowPictureDto> GetAsync(string fileName);
        Task<SlideShowPictureDto> SaveAsync(SlideShowPictureDto dto);
        Task<SlideShowPictureDto> UpdateAsync(string fileName, SlideShowPictureDto dto);
        Task DeleteAsync(string fileName);
    }
}
