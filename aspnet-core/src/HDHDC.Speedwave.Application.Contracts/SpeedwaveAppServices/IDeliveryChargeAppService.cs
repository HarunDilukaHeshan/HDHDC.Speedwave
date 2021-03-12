using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IDeliveryChargeAppService : IApplicationService, ITransientDependency
    {
        Task<DeliveryChargeDto> CalculateAsync(int addressId, int deliveryScheduleId, CartItemDto[] items);
    }
}
