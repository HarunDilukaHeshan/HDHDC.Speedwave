using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IManagerRepository : IRepository<ManagerEntity, int>
    {
        Task<ManagerEntity[]> GetArrayAsync(bool includeDetails = false);
        Task<IList<ManagerEntity>> GetListAsync(bool includeDetails = false);
        Task<ManagerEntity[]> GetByDistrictIdAsync(string id, bool includeDetails = false);
    }
}
