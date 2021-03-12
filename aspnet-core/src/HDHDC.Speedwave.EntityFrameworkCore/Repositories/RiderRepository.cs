using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using HDHDC.Speedwave.SpeedyConsts;
using Volo.Abp;

namespace HDHDC.Speedwave.Repositories
{
    public class RiderRepository : EfCoreRepository<SpeedwaveDbContext, RiderEntity, int>, IRiderRepository
    {
        public RiderRepository(IDbContextProvider<SpeedwaveDbContext> provider)
            : base(provider)
        { }

        public virtual async Task<IList<RiderEntity>> GetListAsync(int skipCount = 0, int maxResultCount = 10, bool includeDetails = false)
        {
            IList<RiderEntity> riderList = new List<RiderEntity>();

            riderList = await (from riderE in DbContext.RiderEntities
                               select riderE)
                               .Skip(skipCount)
                               .Take(maxResultCount)
                               .IncludeIf(includeDetails, e => e.AppUser)
                               .IncludeIf(includeDetails, e => e.CityEntity)
                               .ToListAsync();

            return riderList;
        }

        public virtual async Task<IList<RiderEntity>> GetListAsync(int cityId, int skipCount = 0, int maxResultCount = 10, bool includeDetails = false)
        {
            IList<RiderEntity> riderList = new List<RiderEntity>();

            riderList = await (from riderE in DbContext.RiderEntities
                               where riderE.CityID == cityId
                               select riderE)
                        .Skip(skipCount)
                        .Take(maxResultCount)
                        .IncludeIf(includeDetails, e => e.AppUser)
                        .IncludeIf(includeDetails, e => e.CityEntity)
                        .ToListAsync();

            return riderList;
        }

        public virtual async Task<IList<RiderEntity>> GetRidersForTheCity(int cityId)
        {
            var riders = await (from riderE in DbContext.RiderEntities
                                join riderCoverage in DbContext.RiderCoverageEntities on riderE.Id equals riderCoverage.RiderID
                                where riderE.IsDeleted == false
                                select riderE)
                         .ToListAsync();

            return riders;
        }

        public virtual async Task<IList<RiderEntity>> SearchAsync(string words, string districtId, int skipCount = 0, int maxResultCount = 10)
        {
            if (words == null || districtId == null) throw new ArgumentNullException();

            var riderEs = DbContext.RiderEntities
            .FromSqlRaw(" SELECT riderE.* FROM Speedy.searchForRider({0}, {1}, {2}, {3}) AS idE" +
            " INNER JOIN Speedy.RiderEntity AS riderE ON idE.Id = riderE.Id", words, districtId, skipCount, maxResultCount)
            .Include(e => e.AppUser)
            .Include(e => e.CityEntity)
            .ToArrayAsync();

            return await riderEs;
        }

        

    }
}
