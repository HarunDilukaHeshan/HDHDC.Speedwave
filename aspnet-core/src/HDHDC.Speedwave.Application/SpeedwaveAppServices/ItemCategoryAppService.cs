using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class ItemCategoryAppService 
        : AbstractKeyCrudAppService<
            ItemCategoryEntity,
            ItemCategoryDto,
            ItemCategoryKey,
            PagedAndSortedResultRequestDto>,
        IItemCategoryAppService
    {
        public ItemCategoryAppService(IRepository<ItemCategoryEntity> repository)
            : base(repository)
        { }

        protected override async Task DeleteByIdAsync(ItemCategoryKey id)
        {
            await Repository.DeleteAsync(e => e.ItemID == id.ItemID && e.CategoryID == id.CategoryID);        
        }

        protected override async Task<ItemCategoryEntity> GetEntityByIdAsync(ItemCategoryKey id)
        {
            return await Repository.GetAsync(e => e.ItemID == id.ItemID && e.CategoryID == id.CategoryID);
        }
    }
}
