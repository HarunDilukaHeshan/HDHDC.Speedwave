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
    public class PaymentMethodDataSeeder
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<PaymentMethodEntity, string> _paymentMethodRepository;

        public PaymentMethodDataSeeder(IRepository<PaymentMethodEntity, string> paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _paymentMethodRepository.GetCountAsync() > 0) return;

            await _paymentMethodRepository.InsertAsync(new PaymentMethodEntity(PaymentMethodConsts.Cash), autoSave: true);
            await _paymentMethodRepository.InsertAsync(new PaymentMethodEntity(PaymentMethodConsts.Card), autoSave: true);
        }
    }
}

