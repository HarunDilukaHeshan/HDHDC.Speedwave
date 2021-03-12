using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.InternalService.Services
{
    public interface IPaymentCalculator : IInternalService, ITransientDependency
    {
        PaymentEntity Calculate(int orderID, float subtotal, DeliveryChargeEntity dChargeE);
    }
}
