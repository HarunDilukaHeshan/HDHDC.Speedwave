using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(OrderCreateDto orderCreateDto);
        Task<OrderDto[]> GetOrdersForManagerAsync(int cityId = 0, int skipCount = 0, int maxResultCount = 10);
        Task<OrderDto[]> GetActiveOrdersForManagerAsync(int cityId = 0, int skipCount = 0, int maxResultCount = 10);
        Task<OrderDto[]> GetOrdersForRiderAsync(int skipCount = 0, int maxResultCount = 10);
        Task<OrderDto[]> GetActiveOrdersForRiderAsync(int skipCount = 0, int maxResultCount = 10);
        Task<OrderDto[]> GetActiveOrdersForCustomerAsync();
        Task<OrderDto[]> GetOrdersForCustomerAsync(int skipCount = 0, int maxResultCount = 10);
        Task<OrderDto[]> GetSelectedOrdersForRiderAsync();
        Task SelectOrderAsync(int orderId);
        Task DeselectOrderAsync(int orderId);
        Task MarkAsDeliveredOrderAsync(int orderId, PaymentDetailDto[] payments);
        Task<OrderDto> GetAsync(int id);
        Task<CancellationReasonDto[]> GetAllCancellationReasonAsync();
        Task<CancelledOrderDto> SendCancellationRequestAsync(CancelledOrderDto dto);
        Task<OrderDto> RollbackCancellationRequestAsync(int orderId);
        Task RemoveAsync(int id);
    }
}
