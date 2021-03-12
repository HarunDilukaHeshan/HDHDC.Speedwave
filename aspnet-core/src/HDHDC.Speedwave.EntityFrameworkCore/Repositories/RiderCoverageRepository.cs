using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp;

namespace HDHDC.Speedwave.Repositories
{
    public class RiderCoverageRepository : EfCoreRepository<SpeedwaveDbContext, RiderCoverageEntity>, IRiderCoverageRepository
    {
       // protected IDataFilter DataFilter { get; }

        public RiderCoverageRepository(
            IDataFilter dataFilter,
            IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        {
          //  this.DataFilter = dataFilter;
          //DataFilter
        }

        public override async Task<RiderCoverageEntity> InsertAsync(RiderCoverageEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            RiderCoverageEntity riderCE = null;

            using (DataFilter.Disable<ISoftDelete>())
            {
                riderCE = await DbContext.RiderCoverageEntities
                    .SingleOrDefaultAsync(e => e.RiderID == entity.RiderID && e.CityID == entity.CityID);

                if (riderCE != null)
                {
                    riderCE.IsDeleted = false;
                    await DbContext.SaveChangesAsync();
                }
            }

            return riderCE;
        }
    }
}
