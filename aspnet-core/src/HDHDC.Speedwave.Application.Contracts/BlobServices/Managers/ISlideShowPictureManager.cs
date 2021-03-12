using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices.Managers
{
    public interface ISlideShowPictureManager : ITransientDependency
    {
        Task<SlideShowPictureDto> GetAsync(string name);
        Task<SlideShowPictureDto[]> GetArrayAsync();
        Task<SlideShowPictureDto> SaveXmlAsync(SlideShowPictureDto dto);
        Task<SlideShowPictureDto> UpdateAsync(string fileName, SlideShowPictureDto dto);
        Task DeleteAsync(string fileName);
    }
}
