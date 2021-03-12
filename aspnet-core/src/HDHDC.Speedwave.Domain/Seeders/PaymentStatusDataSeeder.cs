using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Seeders
{
    public class PaymentStatusDataSeeder
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<PaymentStatusEntity, string> _paymentStatusRepository;

        public PaymentStatusDataSeeder(IRepository<PaymentStatusEntity, string> paymentStatusRepository)
        {
            _paymentStatusRepository = paymentStatusRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _paymentStatusRepository.GetCountAsync() > 0) return;

            await _paymentStatusRepository.InsertAsync(new PaymentStatusEntity(PaymentStatusConsts.Pending), autoSave: true);
            await _paymentStatusRepository.InsertAsync(new PaymentStatusEntity(PaymentStatusConsts.Paid), autoSave: true);
            await _paymentStatusRepository.InsertAsync(new PaymentStatusEntity(PaymentStatusConsts.Cancelled), autoSave: true);
        }
    }
}
