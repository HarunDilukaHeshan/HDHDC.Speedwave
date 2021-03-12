using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IOrderRepository : IRepository<OrderEntity, int>
    {
        Task<OrderEntity[]> GetOrdersForRiderAsync(Guid userId, int skipCount = 0, int maxResultCount = 10);
        Task<OrderEntity[]> GetActiveOrdersForRiderAsync(Guid userId, int skipCount = 0, int maxResultCount = 10);
        Task<OrderEntity[]> GetOrdersForManagerAsync(Guid userId, int cityId = 0, int skipCount = 0, int maxResultCount = 10);
        Task<OrderEntity[]> GetActiveOrdersForManagerAsync(Guid userId, int cityId = 0, int skipCount = 0, int maxResultCount = 10);
        Task<OrderEntity[]> GetActiveOrdersForCustomerAsync(Guid userId);
        Task<OrderEntity[]> GetOrdersForCustomerAsync(Guid userId, int skipCount = 0, int maxResultCount = 10);
        Task<OrderEntity[]> GetSelectedOrdersForRiderAsync(Guid userId);
        Task SelectOrderAsync(Guid userId, int orderId);
        Task DeselectOrderAsync(Guid userId, int orderId);
        Task MarkAsDeliveredOrderAsync(Guid userId, int orderId, PaymentDetailEntity[] payments);
        Task<OrderEntity> GetOrderForRiderAsync(Guid userId, int id);
        Task<OrderEntity> GetOrderForCustomerAsync(Guid userId, int id);
        Task<OrderEntity> GetOrderForManagerAsync(Guid userId, int id);
        Task<CancellationReasonEntity[]> GetAllCancellationReasonAsync();
        Task<CancelledOrderEntity> SendCancellationRequestAsync(Guid userId, CancelledOrderEntity entity);
        Task<OrderEntity> RollbackCancellationRequestAsync(Guid userId, int id);
        Task RemoveAsync(Guid userId, int id);
    }
}
