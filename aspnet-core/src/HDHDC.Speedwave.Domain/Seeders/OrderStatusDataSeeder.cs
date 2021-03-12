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
    public class OrderStatusDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<OrderStatusEntity, string> _orderStatusRepository;

        public OrderStatusDataSeeder(IRepository<OrderStatusEntity, string> orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {            
            if (await _orderStatusRepository.GetCountAsync() > 0) return;
            
            await _orderStatusRepository.InsertAsync(new OrderStatusEntity(OrderStatusConsts.Pending), autoSave: true);
            await _orderStatusRepository.InsertAsync(new OrderStatusEntity(OrderStatusConsts.Inprogress), autoSave: true);
            await _orderStatusRepository.InsertAsync(new OrderStatusEntity(OrderStatusConsts.Delivered), autoSave: true);
            await _orderStatusRepository.InsertAsync(new OrderStatusEntity(OrderStatusConsts.Cancelled), autoSave: true);
            await _orderStatusRepository.InsertAsync(new OrderStatusEntity(OrderStatusConsts.Haulted), autoSave: true);
        }
    }
}
