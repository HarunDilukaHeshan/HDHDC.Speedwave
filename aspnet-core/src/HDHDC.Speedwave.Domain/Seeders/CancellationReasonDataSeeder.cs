using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Seeders
{
    public class CancellationReasonDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<CancellationReasonEntity, int> _cancellationReasonEntity;

        public CancellationReasonDataSeeder(IRepository<CancellationReasonEntity, int> cancellationReason)
        {
            _cancellationReasonEntity = cancellationReason;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _cancellationReasonEntity.GetCountAsync() > 0) return;

            await _cancellationReasonEntity.InsertAsync(new CancellationReasonEntity
            {
                CancellationReason = "CouldNotReachTheCustomer",
                Description = "The customer could not be able to contacted"
            });

            await _cancellationReasonEntity.InsertAsync(new CancellationReasonEntity
            {
                CancellationReason = "OrderedItemAreNotAvailable",
                Description = "Some or all of the ordered items are not available"
            });

            await _cancellationReasonEntity.InsertAsync(new CancellationReasonEntity
            {
                CancellationReason = "CustomerRejection",
                Description = "The customer rejected the order"
            });
            await _cancellationReasonEntity.InsertAsync(new CancellationReasonEntity
            {
                CancellationReason = "Other",
                Description = "Some other reason"
            });
        }
    }
}
