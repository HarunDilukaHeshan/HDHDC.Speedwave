using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class QuantityRepository : EfCoreRepository<SpeedwaveDbContext, QuantityEntity, int>, IQuantityRepository
    {
        public QuantityRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }
        
        public override async Task<QuantityEntity> InsertAsync(QuantityEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var unitEntity = DbContext.UnitEntities.Single(e => e.Id == entity.UnitID);
            var unitSymbol = unitEntity.UnitSymbol;

            entity.NormalizedQuantityLabel = string.Format("{0} {1}", entity.Quantity, unitSymbol);
            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }
    }
}
