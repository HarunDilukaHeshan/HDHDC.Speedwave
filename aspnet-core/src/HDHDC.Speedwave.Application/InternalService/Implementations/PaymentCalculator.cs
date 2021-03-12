using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.InternalService.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.InternalService.Implementations
{
    internal class PaymentCalculator : IPaymentCalculator
    {
        public PaymentCalculator(
            IRepository<ItemEntity, int> itemRepository)
        {
            ItemRepository = itemRepository;
        }

        protected IRepository<ItemEntity, int> ItemRepository { get; }

        public virtual PaymentEntity Calculate(int orderID, float subtotal, DeliveryChargeEntity dChargeE)
        {            
            var paymentE = new PaymentEntity(orderID, dChargeE.Id, subtotal + dChargeE.Charge, subtotal);

            return paymentE;
        }
    }
}
