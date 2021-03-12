using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IRiderAppService : IApplicationService
    {
        Task<RiderDto> GetCurrentRiderAsync();
        Task<CityDto[]> GetRiderCoverageAsync();
        Task<RiderDto> GetAsync(int id);
        Task<RiderDto> GetRiderByUserIdAsync(Guid userId);
        Task<RiderDto[]> GetListAsync(PagedAndSortedResultRequestDto input, bool includeDetails);
        Task<RiderDto[]> GetListAsync(int cityId, PagedAndSortedResultRequestDto input, bool includeDetails);
        Task<RiderDto> UpdateAsync(int id, RiderUpdateDto dto);
        Task<RiderDto[]> SearchAsync(string words, int skipCount = 0, int maxResultCount = 10);
        Task BlockAsync(int riderId);
        Task WarnFirstAsync(int riderId);
        Task WarnSecondAsync(int riderId);
        Task UnblockAsync(int riderId);
        Task<CityDto[]> UpdateRiderCoverageAsync(int[] cityIds);


    }
}
