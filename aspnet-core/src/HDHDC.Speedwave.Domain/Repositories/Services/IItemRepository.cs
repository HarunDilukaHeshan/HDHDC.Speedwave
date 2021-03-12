using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IItemRepository : IRepository<ItemEntity, int>
    {
        Task<ItemEntity[]> SearchAsync(string keywords = "", int skipCount = 0, int maxResultCount = 10);
        Task<ItemEntity[]> SelectItemsAsync(int seed, int maxResultCount = 10);
        Task<ItemEntity[]> RndSelectWithinTheRadiusAsync(int cityId, int seed, int maxResultCount = 10);
        Task<ItemEntity[]> SearchWithinTheRadiusAsync(int cityId, string keywords = "", int skipCount = 0, int maxResultCount = 10);
        Task<bool> ValidateItemsWithinRadiusAsync(int cityId, int[] itemIds);
    }
}
