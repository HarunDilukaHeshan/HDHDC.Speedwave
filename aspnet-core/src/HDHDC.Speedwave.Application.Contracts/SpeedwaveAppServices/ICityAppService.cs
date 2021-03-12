using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface ICityAppService
        : ICrudAppService<
            CityDto,
            int,
            PagedAndSortedResultRequestDto,
            CityCreateDto,
            CityUpdateDto>
    {
        Task<CityDto[]> SearchAsync(int skipCount = 0, int maxResultCount = 10, string keyword = "");
        Task<CityDto[]> GetListAsync(string districtId);
        Task<CityDto[]> GetCityListWithinTheRadiusAsync(int cityId);
    }
}
