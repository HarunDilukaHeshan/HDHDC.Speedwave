using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class ManagerRepository : EfCoreRepository<SpeedwaveDbContext, ManagerEntity, int>, IManagerRepository
    {
        public ManagerRepository(IDbContextProvider<SpeedwaveDbContext> provider)
            : base(provider)
        { }

        public async Task<ManagerEntity[]> GetArrayAsync(bool includeDetails = false)
        {
            var eArr = await DbContext.ManagerEntities
                .Where(e => e.IsDeleted == false)
                .IncludeIf(includeDetails, e => e.AppUser)
                .IncludeIf(includeDetails, e => e.DistrictEntity)
                .ToArrayAsync();

            return eArr;
        }

        public async Task<IList<ManagerEntity>> GetListAsync(bool includeDetails = false)
        {
            return (await GetArrayAsync(includeDetails)).ToList();
        }

        public async Task<ManagerEntity[]> GetByDistrictIdAsync(string id, bool includeDetails = false)
        {
            var arr = await DbContext.ManagerEntities
                    .Where(e => e.DistrictID == id)
                    .IncludeIf(includeDetails, e => e.AppUser)
                    .IncludeIf(includeDetails, e => e.DistrictEntity)
                    .ToArrayAsync();

            return arr;
        }
    }
}
