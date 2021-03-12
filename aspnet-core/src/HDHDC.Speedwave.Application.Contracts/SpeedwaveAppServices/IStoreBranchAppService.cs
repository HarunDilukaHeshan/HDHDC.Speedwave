using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IStoreBranchAppService
        : ICrudAppService<
            StoreBranchDto,
            int,
            PagedAndSortedResultRequestDto,
            StoreBranchCreateDto,
            StoreBranchUpdateDto>
    {
        Task<StoreOpenDayDto[]> GetAllOpenDayAsync(int branchId);
        Task<StoreOpenDayDto> CreateOpenDayAsync(int branchId, StoreOpenDayDto dto);
        Task<StoreOpenDayDto[]> UpdateOpenDayAsync(int branchId, StoreOpenDayDto[] dtoArr);
        Task DeleteOpenDayAsync(int branchId, DayOfWeek dayOfWeek);
        Task<StoreClosingDateDto[]> GetAllClosingDateAsync(int branchId);
        Task<StoreOpenDayDto> GetOpenDayAsync(int branchId, DayOfWeek dayOfWeek);
        Task<StoreClosingDateDto> CreateClosingDateAsync(int branchId, DateTime closingDate);
        Task DeleteClosingDateAsync(int Id);
        Task<ItemStoreBranchDto[]> GetAllItemsAsync(
            int branchId, 
            int skipCount = 0, 
            int maxResultCount = 10, 
            bool includeDetails = false);
        Task<ItemStoreBranchDto[]> SearchAsync(
            string keywords,
            int branchId,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false);
        Task<ItemStoreBranchDto> AddItemAsync(int branchId, ItemStoreBranchDto dto);
        Task RemoveItemAsync(int branchId, int itemId);
        Task<StoreBranchDto[]> GetBranchesWithinRadius(int cityId);
    }
}
