using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IRiderRepository : IRepository<RiderEntity, int>
    {
        Task<IList<RiderEntity>> GetRidersForTheCity(int cityId);
        Task<IList<RiderEntity>> GetListAsync(int skipCount = 0, int maxResultCount = 10, bool includeDetails = false);
        Task<IList<RiderEntity>> GetListAsync(int cityId, int skipCount = 0, int maxResultCount = 10, bool includeDetails = false);
        Task<IList<RiderEntity>> SearchAsync(string words, string districtId, int skipCount = 0, int maxResultCount = 10);
    }
}
