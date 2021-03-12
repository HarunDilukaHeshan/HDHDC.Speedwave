using HDHDC.Speedwave.BlobServices;
using HDHDC.Speedwave.BlobServices.Managers;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class CategoryAppService : ApplicationService, ICategoryAppService, ITransientDependency
    {
        protected ICategoryThumbnailManager ThumbnailManager { get; }

        protected IRepository<CategoryEntity, int> Repository { get; }

        public CategoryAppService(
            IRepository<CategoryEntity, int> repository,
            ICategoryThumbnailManager thumbnailManager)
        {
            ThumbnailManager = thumbnailManager;
            Repository = repository;
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var fileName = await ThumbnailManager.SaveAsync(categoryCreateDto.ThumbnailBase64);

            var categoryEntity = ObjectMapper.Map<CategoryCreateDto, CategoryEntity>(categoryCreateDto);

            categoryEntity.CategoryThumbnail = fileName;

            await Repository.InsertAsync(categoryEntity, autoSave: true);

            return ObjectMapper.Map<CategoryEntity, CategoryDto>(categoryEntity);
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var categoryE = await Repository.GetAsync(id);

            if (categoryE == null) return null;

            return ObjectMapper.Map<CategoryEntity, CategoryDto>(categoryE);
        }

        public async Task<IList<CategoryDto>> GetAllAsync()
        {
            var categoryList = await Repository.GetListAsync();
            return ObjectMapper.Map<IList<CategoryEntity>, IList<CategoryDto>>(categoryList);
        }

        public async Task DeleteAsync(int id)
        {
            var categoryE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();
            await Repository.DeleteAsync(categoryE);
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var categoryE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            if (!string.IsNullOrWhiteSpace(categoryUpdateDto.ThumbnailBase64))
            {
                var categoryThumbnail = await ThumbnailManager.SaveAsync(categoryUpdateDto.ThumbnailBase64);
                await ThumbnailManager.DeleteAsync(categoryE.CategoryThumbnail);
                categoryE.CategoryThumbnail = categoryThumbnail;
            }

            ObjectMapper.Map(categoryUpdateDto, categoryE);
            await Repository.UpdateAsync(categoryE);

            return ObjectMapper.Map<CategoryEntity, CategoryDto>(categoryE);
        }
        public async Task<string> GetThumbnail(string thumbnailName)
        {
            return await ThumbnailManager.GetAsync(thumbnailName);
        }
    }
}
