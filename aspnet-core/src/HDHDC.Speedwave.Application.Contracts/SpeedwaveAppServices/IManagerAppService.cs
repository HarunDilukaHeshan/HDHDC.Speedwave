using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IManagerAppService : IApplicationService
    {
        Task ChangeDistrictAsync(int managerId, DistrictDto districtDto);        
        Task BlockAsync(int managerId);        
        Task WarnFirstAsync(int managerId);        
        Task WarnSecondAsync(int managerId);        
        Task UnblockAsync(int managerId);        
        Task<ManagerDto> GetManagerByUserIdAsync(Guid userId);
        Task<ManagerDto[]> GetAllManagerByDistrictIdAsync(string districtId, bool includeDetails = false);
        Task<ManagerDto[]> GetAllAsync(bool includeDetails = false);
        Task<ManagerDto> GetAsync(int id);
    }
}
