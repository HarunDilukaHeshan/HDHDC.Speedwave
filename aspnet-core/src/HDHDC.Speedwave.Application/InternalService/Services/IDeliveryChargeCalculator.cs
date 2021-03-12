using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDHDC.Speedwave.InternalService.Services
{
    public interface IDeliveryChargeCalculator : IInternalService
    {
        Task<DeliveryChargeEntity> CalculateAsync(int addressId, int deliveryScheduleId, float subtotal, bool hasPromotion = false);
    }
}
