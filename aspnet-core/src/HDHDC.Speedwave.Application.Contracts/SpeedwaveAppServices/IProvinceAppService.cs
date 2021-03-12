using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IProvinceAppService : IApplicationService
    {
        Task<ProvinceDto> CreateAsync(ProvinceDto dto);

        Task<ProvinceDto> GetAsync(string provinceId);

        Task<ProvinceDto[]> GetListAsync(PagedAndSortedResultRequestDto input);

        Task DeleteAsync(string id);
    }
}
