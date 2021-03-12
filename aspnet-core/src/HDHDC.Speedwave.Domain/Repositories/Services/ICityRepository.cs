using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface ICityRepository : IRepository<CityEntity, int>
    {
        Task<CityEntity[]> SearchAsync(int skipCount = 0, int maxResultCount = 10, string keyword = "");
        Task<CityEntity[]> GetListAsync(string districtId);
        Task<CityEntity[]> GetCityListWithinTheRadiusAsync(int cityId, float radius);
    }
}
