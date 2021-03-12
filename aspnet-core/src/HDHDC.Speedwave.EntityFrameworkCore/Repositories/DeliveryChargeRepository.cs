using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace HDHDC.Speedwave.Repositories
{
    public class DeliveryChargeRepository : EfCoreRepository<SpeedwaveDbContext, DeliveryChargeEntity, int>, 
        IDeliveryChargeRepository
    {
        public DeliveryChargeRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }

        public async Task<RiderMeanDistanceKeylessEntity> GetMeanDistanceAsync(int addressId)
        {
            _ = await DbContext.AddressEntities.SingleOrDefaultAsync(e => e.Id == addressId) 
                ?? throw new BusinessException();

            var meanDistance = await DbContext.MeanDistance
                .FromSqlRaw("SELECT * FROM [Speedy].[getRiderMeanDistance]({0})", addressId)
                .SingleOrDefaultAsync();

            return meanDistance;
        }
    }
}
