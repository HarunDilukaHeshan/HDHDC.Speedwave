using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface ICategoryAppService : ITransientDependency
    {
        Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task DeleteAsync(int id);
        Task<CategoryDto> GetAsync(int id);
        Task<IList<CategoryDto>> GetAllAsync();
        Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto categoryCreateUpdateDto);
        Task<string> GetThumbnail(string thumbnailName);
    }
}
