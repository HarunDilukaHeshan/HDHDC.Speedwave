using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Options;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class DeliveryScheduleRepository : EfCoreRepository<SpeedwaveDbContext, DeliveryScheduleEntity, int>, 
        IDeliveryScheduleRepository
    {
        protected GeoDistanceOptions GeoDistanceOptions { get; }

        public DeliveryScheduleRepository(
            IDbContextProvider<SpeedwaveDbContext> provider, 
            IOptions<GeoDistanceOptions> geoDistanceOptions)
             : base(provider)
        {
            this.GeoDistanceOptions = geoDistanceOptions.Value;
        }

        public virtual async Task<DeliveryScheduleEntity[]> GetCompatibleSchedules(Guid userId, int addressId, int[] selectedItems)
        {
            var addrE = await (from addrR in DbContext.AddressEntities
                         join cusR in DbContext.CustomerEntities on addrR.CustomerID equals cusR.Id
                         //join userR in DbContext.Users on cusR.UserID equals userR.Id
                         where addrR.IsDeleted == false && addrR.Id == addressId
                         select addrR)
                        .SingleOrDefaultAsync();

            _ = addrE ?? throw new BusinessException();
            var itemIdsStr = string.Join(',', selectedItems);

            var dsArr = new DeliveryScheduleEntity[0];

            try
            {
                var cdsRArr = DbContext.DeliveryScheduleEntities
                    .FromSqlRaw("SELECT dsR.* FROM [Speedy].[getCompatibleDsSchedules]({0}, {1}, {2}, {3}) AS cdsR INNER JOIN [Speedy].[DeliveryScheduleEntity] AS dsR on cdsR.Id = dsR.Id",
                    addrE.Id,
                    itemIdsStr,
                    GeoDistanceOptions.MaxRadius,
                    DateTime.Now);

                dsArr = await cdsRArr.ToArrayAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException();
            }

            return dsArr;
        }
    }
}

// addressId
// selected items