using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HDHDC.Speedwave.SpeedyConsts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories
{
    public class OrderRepository : EfCoreRepository<SpeedwaveDbContext, OrderEntity, int>, IOrderRepository
    {
        private IRepository<CancelledOrderEntity, int> CancelledOrderRepository { get; set; }

        public OrderRepository(
            IRepository<CancelledOrderEntity, int> cancelledOrderRepository,
            IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        {
            this.CancelledOrderRepository = cancelledOrderRepository;
        }

        public virtual async Task<OrderEntity[]> GetOrdersForRiderAsync(Guid userId, int skipCount = 0, int maxResultCount = 10)
        {
            if (userId == null) throw new ArgumentNullException();
            if (skipCount < 0 || maxResultCount < 0) throw new ArgumentException();

            var orders = await (from oR in DbContext.OrderEntities
                                join rR in DbContext.RiderEntities on oR.RiderID equals rR.Id
                                where rR.UserID == userId && rR.Status != EntityStatusConsts.Blocked && rR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .OrderBy(e => e.CreationTime)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .ToArrayAsync();

            return orders;
        }

        public virtual async Task<OrderEntity[]> GetActiveOrdersForRiderAsync(Guid userId, int skipCount = 0, int maxResultCount = 10)
        {
            if (userId == null) throw new ArgumentNullException();
            if (skipCount < 0 || maxResultCount < 0) throw new ArgumentException();

            var orders = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join rcR in DbContext.RiderCoverageEntities on addrR.CityID equals rcR.CityID
                                join rR in DbContext.RiderEntities on rcR.RiderID equals rR.Id
                                where rR.UserID == userId && rR.Status != EntityStatusConsts.Blocked && rR.Status != EntityStatusConsts.Inactive
                                    && oR.OrderStatus == OrderStatusConsts.Pending
                                select oR)
                      .OrderBy(e => e.CreationTime)
                      .Skip(skipCount)
                      .Take(maxResultCount)
                      .Include(e => e.AddressEntity)
                      .ThenInclude(e => e.CityEntity)
                      .Include(e => e.PaymentEntity)
                      .ThenInclude(e => e.DeliveryChargeEntity)
                      .Include(e => e.DeliveryScheduleEntity)
                      .Include(e => e.PromotionEntity)
                      .Include(e => e.RiderEntity)
                      .ToArrayAsync();

            return orders;
        }

        public virtual async Task<OrderEntity[]> GetSelectedOrdersForRiderAsync(Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();

            var orders = await (from oR in DbContext.OrderEntities
                                join rR in DbContext.RiderEntities on oR.RiderID equals rR.Id
                                where rR.UserID == userId && 
                                    (oR.OrderStatus == OrderStatusConsts.Inprogress || oR.OrderStatus == OrderStatusConsts.Haulted)
                                   && rR.Status != EntityStatusConsts.Blocked && rR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .ToArrayAsync();

            return orders;
        }

        public virtual async Task<OrderEntity[]> GetOrdersForManagerAsync(Guid userId, int cityId = 0, int skipCount = 0, int maxResultCount = 10)
        {
            if (userId == null) throw new ArgumentNullException();
            if (skipCount < 0 || maxResultCount < 0) throw new ArgumentException();

            OrderEntity[] orders = new OrderEntity[0];

            if (cityId == 0)
            {
                orders = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join dR in DbContext.DistrictEntities on cityR.DistrictID equals dR.Id
                                join mR in DbContext.ManagerEntities on dR.Id equals mR.DistrictID
                                where mR.UserID == userId && mR.Status != EntityStatusConsts.Blocked && mR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .OrderBy(e => e.CreationTime)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .ToArrayAsync();
            }
            else
            {
                orders = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join dR in DbContext.DistrictEntities on cityR.DistrictID equals dR.Id
                                join mR in DbContext.ManagerEntities on dR.Id equals mR.DistrictID
                                where mR.UserID == userId && cityR.Id == cityId && mR.Status != EntityStatusConsts.Blocked && mR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .OrderBy(e => e.CreationTime)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .ToArrayAsync();
            }

            return orders;
        }

        public virtual async Task<OrderEntity[]> GetActiveOrdersForManagerAsync(Guid userId, int cityId = 0, int skipCount = 0, int maxResultCount = 10)
        {
            if (userId == null) throw new ArgumentNullException();
            if (skipCount < 0 || maxResultCount < 0) throw new ArgumentException();

            OrderEntity[] orders = new OrderEntity[0];

            if (cityId == 0)
            {
                orders = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join dR in DbContext.DistrictEntities on cityR.DistrictID equals dR.Id
                                join mR in DbContext.ManagerEntities on dR.Id equals mR.DistrictID
                                where mR.UserID == userId && (oR.OrderStatus == OrderStatusConsts.Pending || oR.OrderStatus == OrderStatusConsts.Inprogress)
                                && mR.Status != EntityStatusConsts.Blocked && mR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .OrderBy(e => e.CreationTime)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .ToArrayAsync();
            }
            else
            {
                orders = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join dR in DbContext.DistrictEntities on cityR.DistrictID equals dR.Id
                                join mR in DbContext.ManagerEntities on dR.Id equals mR.DistrictID
                                where mR.UserID == userId && cityR.Id == cityId && (oR.OrderStatus == OrderStatusConsts.Pending || oR.OrderStatus == OrderStatusConsts.Inprogress)
                                && mR.Status != EntityStatusConsts.Blocked && mR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .OrderBy(e => e.CreationTime)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .ToArrayAsync();
            }

            return orders;
        }

        public virtual async Task<OrderEntity[]> GetActiveOrdersForCustomerAsync(Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();

            OrderEntity[] orders = new OrderEntity[0];

            var orderEArr = await (from oR in DbContext.OrderEntities
                                   join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                   join cusR in DbContext.CustomerEntities on addrR.CustomerID equals cusR.Id
                                   where cusR.UserID == userId && cusR.Status != EntityStatusConsts.Blocked && cusR.Status != EntityStatusConsts.Inactive
                                      && (oR.OrderStatus == OrderStatusConsts.Inprogress || oR.OrderStatus == OrderStatusConsts.Pending)
                                   select oR)
                                   .Include(e => e.AddressEntity)
                                   .ThenInclude(e => e.CityEntity)
                                   .Include(e => e.DeliveryScheduleEntity)
                                   .Include(e => e.PaymentEntity)
                                   .ThenInclude(e => e.DeliveryChargeEntity)
                                   .Include(e => e.PromotionEntity)
                                   .ToArrayAsync();

            return orderEArr;
        }

        public virtual async Task<OrderEntity[]> GetOrdersForCustomerAsync(Guid userId, int skipCount = 0, int maxResultCount = 10)
        {
            if (userId == null) throw new ArgumentNullException();

            OrderEntity[] orders = new OrderEntity[0];

            var orderEArr = await (from oR in DbContext.OrderEntities
                                   join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                   join cusR in DbContext.CustomerEntities on addrR.CustomerID equals cusR.Id
                                   where cusR.UserID == userId && cusR.Status != EntityStatusConsts.Blocked && cusR.Status != EntityStatusConsts.Inactive
                                   select oR)
                                   .OrderBy(e => e.CreationTime)
                                   .Skip(skipCount)
                                   .Take(maxResultCount)
                                   .Include(e => e.AddressEntity)
                                   .ThenInclude(e => e.CityEntity)
                                   .Include(e => e.DeliveryScheduleEntity)
                                   .Include(e => e.PaymentEntity)
                                   .ThenInclude(e => e.DeliveryChargeEntity)
                                   .Include(e => e.PromotionEntity)
                                   .ToArrayAsync();

            return orderEArr;
        }

        public virtual async Task SelectOrderAsync(Guid userId, int orderId)
        {
            var riderE = await DbContext.RiderEntities
                .SingleOrDefaultAsync(e => e.UserID == userId && e.Status != EntityStatusConsts.Blocked && e.Status != EntityStatusConsts.Inactive);

            _ = riderE ?? throw new BusinessException();

            if ((await DbContext.OrderEntities
                .CountAsync(e => e.RiderID == riderE.Id && 
                    (e.OrderStatus == OrderStatusConsts.Inprogress || e.OrderStatus == OrderStatusConsts.Haulted))) > 1)
                throw new BusinessException();

            var orderE = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join rcR in DbContext.RiderCoverageEntities on cityR.Id equals rcR.CityID
                                join riderR in DbContext.RiderEntities on rcR.RiderID equals riderR.Id
                                where riderR.UserID == userId && oR.Id == orderId && oR.OrderStatus == OrderStatusConsts.Pending
                                select oR)
                                .SingleOrDefaultAsync();

            _ = orderE ?? throw new BusinessException();

            orderE.RiderID = riderE.Id;
            orderE.OrderStatus = OrderStatusConsts.Inprogress;

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeselectOrderAsync(Guid userId, int orderId)
        {
            var riderE = await DbContext.RiderEntities
                .SingleOrDefaultAsync(e => e.UserID == userId && e.Status != EntityStatusConsts.Blocked && e.Status != EntityStatusConsts.Inactive);

            _ = riderE ?? throw new BusinessException();

            var orderE = await DbContext.OrderEntities
                .SingleOrDefaultAsync(e => e.Id == orderId && e.OrderStatus == OrderStatusConsts.Inprogress && e.RiderID == riderE.Id) 
                ?? throw new BusinessException();

            orderE.RiderID = null;
            orderE.OrderStatus = OrderStatusConsts.Pending;

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task MarkAsDeliveredOrderAsync(Guid userId, int orderId, PaymentDetailEntity[] payments)
        {
            var orderE = await (from oR in DbContext.OrderEntities
                                join riderR in DbContext.RiderEntities on oR.RiderID equals riderR.Id
                                where riderR.UserID == userId && oR.OrderStatus == OrderStatusConsts.Inprogress &&
                                   riderR.Status != EntityStatusConsts.Blocked && riderR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .SingleOrDefaultAsync();

            var paymentDetailList = new List<PaymentDetailEntity>();
            var totalPaid = 0.0f;

            foreach (var p in payments)
            {
                paymentDetailList.Add(new PaymentDetailEntity(orderE.PaymentEntity.Id, p.PaymentMethod, p.TotalPaid));
                totalPaid += p.TotalPaid;
            }

            if (totalPaid < orderE.PaymentEntity.DeliveryChargeEntity.Charge) throw new BusinessException();

            orderE.PaymentEntity.TotalPaid = totalPaid;
            orderE.OrderStatus = OrderStatusConsts.Delivered;

            await DbContext.PaymentEntities.AddRangeAsync((IEnumerable<PaymentEntity>)paymentDetailList);
            await DbContext.SaveChangesAsync();
        }        

        public virtual async Task<OrderEntity> GetOrderForRiderAsync(Guid userId, int id)
        {
            var orderE = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join rcR in DbContext.RiderCoverageEntities on cityR.Id equals rcR.CityID
                                join riderR in DbContext.RiderEntities on rcR.RiderID equals riderR.Id
                                where oR.Id == id && riderR.UserID == userId && riderR.Status != EntityStatusConsts.Blocked && riderR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .Include(e => e.OrderItemEntities)
                                .Include("OrderItemEntities.ItemEntity")
                                .SingleOrDefaultAsync() ?? throw new BusinessException();

            return orderE;
        }

        public virtual async Task<OrderEntity> GetOrderForCustomerAsync(Guid userId, int id)
        {
            var orderE = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cusR in DbContext.CustomerEntities on addrR.CustomerID equals cusR.Id
                                where cusR.UserID == userId && oR.Id == id &&
                                    cusR.Status != EntityStatusConsts.Blocked && cusR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .Include(e => e.OrderItemEntities)
                                .Include("OrderItemEntities.ItemEntity")
                                .SingleOrDefaultAsync() ?? throw new BusinessException();

            return orderE;
        }

        public virtual async Task<OrderEntity> GetOrderForManagerAsync(Guid userId, int id)
        {
            var orderE = await (from oR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on oR.AddressID equals addrR.Id
                                join cityR in DbContext.CityEntities on addrR.CityID equals cityR.Id
                                join mR in DbContext.ManagerEntities on cityR.DistrictID equals mR.DistrictID
                                where mR.UserID == userId && oR.Id == id &&
                                   mR.Status != EntityStatusConsts.Blocked && mR.Status != EntityStatusConsts.Inactive
                                select oR)
                                .Include(e => e.AddressEntity)
                                .ThenInclude(e => e.CityEntity)
                                .Include(e => e.DeliveryScheduleEntity)
                                .Include(e => e.PaymentEntity)
                                .ThenInclude(e => e.DeliveryChargeEntity)
                                .Include(e => e.PromotionEntity)
                                .Include(e => e.OrderItemEntities)
                                .Include("OrderItemEntities.ItemEntity")
                                .SingleOrDefaultAsync() ?? throw new BusinessException();

            return orderE;
        }

        public virtual async Task<CancellationReasonEntity[]> GetAllCancellationReasonAsync()
        {
            return await DbContext.CancellationReasonEntities.Where(e => e.IsDeleted == false)
                .ToArrayAsync();
        }

        public virtual async Task<CancelledOrderEntity> SendCancellationRequestAsync(Guid userId, CancelledOrderEntity entity)
        {
            var riderE = await DbContext.RiderEntities
                .SingleOrDefaultAsync(e => e.UserID == userId && e.Status != EntityStatusConsts.Blocked
                && e.Status != EntityStatusConsts.Inactive) ?? throw new BusinessException();

            var crE = await DbContext.CancellationReasonEntities.SingleOrDefaultAsync(e => e.Id == entity.CancellationReasonId)
                ?? throw new BusinessException();

            var oE = await (from oR in DbContext.OrderEntities
                            where oR.RiderID == riderE.Id && oR.Id == entity.Id
                               && oR.OrderStatus == OrderStatusConsts.Inprogress
                            select oR)
                            .SingleOrDefaultAsync() ?? throw new BusinessException();

            var ocE = new CancelledOrderEntity(oE.Id)
            {
                CancellationReasonId = entity.CancellationReasonId,
                Description = entity.Description,
                IsApproved = false
            };

            oE.OrderStatus = OrderStatusConsts.Haulted;

            await DbContext.CancelledOrderEntities.AddAsync(ocE);
            await DbContext.SaveChangesAsync();

            return ocE;
        }   

        public virtual async Task<OrderEntity> RollbackCancellationRequestAsync(Guid userId, int id)
        {
            var riderE = await DbContext.RiderEntities
                .SingleOrDefaultAsync(e => e.UserID == userId &&
                e.Status != EntityStatusConsts.Blocked && e.Status != EntityStatusConsts.Blocked)
                ?? throw new BusinessException();

            var oE = await (from oR in DbContext.OrderEntities
                            join crR in DbContext.CancelledOrderEntities on oR.Id equals crR.Id
                            where oR.OrderStatus == OrderStatusConsts.Haulted && oR.RiderID == riderE.Id &&
                               crR.IsApproved == false && crR.ApproverId == null
                            select oR)
                            .SingleOrDefaultAsync() ?? throw new BusinessException();                                    
            
            oE.OrderStatus = OrderStatusConsts.Inprogress;
            
            var coE = await DbContext.CancelledOrderEntities.SingleOrDefaultAsync(e => e.Id == oE.Id);

            await DbContext.SaveChangesAsync();
            await CancelledOrderRepository.HardDeleteAsync(coE, true);                                        

            return oE;
        }

        public virtual async Task RemoveAsync(Guid userId, int id)
        {
            if (userId == null || id < 1) throw new ArgumentException();

            var cusE = await DbContext.CustomerEntities
                .SingleOrDefaultAsync(e => e.UserID == userId) ?? throw new BusinessException();

            var orderE = await (from orderR in DbContext.OrderEntities
                                join addrR in DbContext.AddressEntities on orderR.AddressID equals addrR.Id
                                join cusR in DbContext.CustomerEntities on addrR.CustomerID equals cusR.Id
                                where cusR.UserID == userId && orderR.OrderStatus == OrderStatusConsts.Pending &&
                                    orderR.Id == id
                                select orderR)
                                .SingleOrDefaultAsync() ?? throw new BusinessException();

            orderE.OrderStatus = OrderStatusConsts.Cancelled;
            await DbContext.SaveChangesAsync();

            DbContext.OrderEntities.Remove(orderE);
            await DbContext.SaveChangesAsync();
        }
    }
}
