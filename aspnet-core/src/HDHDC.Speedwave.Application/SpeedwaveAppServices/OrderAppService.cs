using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.InternalService.Services;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HDHDC.Speedwave.SpeedyConsts;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        protected IRepository<CustomerEntity, int> CustomerRepository { get; }
        protected IRepository<OrderItemEntity> OrderItemRepository { get; }
        protected IOrderRepository OrderRepository { get; }
        protected IRepository<AddressEntity, int> AddressRepository { get; }
        protected IRepository<DeliveryScheduleEntity, int> DeliveryScheduleRepository { get; }
        protected IRepository<PromotionEntity, int> PromotionRepository { get; }
        protected IItemRepository ItemRepository { get; }
        protected IRepository<DeliveryChargeEntity, int> DeliveryChargeRepository { get; }
        protected IRepository<PaymentEntity, int> PaymentRepository { get; }
        protected IDeliveryChargeCalculator DeliveryChargeCalculator { get; }
        protected IPaymentCalculator PaymentCalculator { get; }
        protected IDeliveryScheduleAppService DeliveryScheduleAppService { get; }
        protected IDeliveryChargeAppService DeliveryChargeAppService { get; }
        protected IRiderRepository RiderRepository { get; }
        public OrderAppService(
            IDeliveryChargeAppService deliveryChargeAppService,            
            IDeliveryScheduleAppService deliveryScheduleAppService,
            IItemRepository itemRepository,
            IRiderRepository riderRepository,
            IRepository<DeliveryChargeEntity, int> deliveryChargeRepository,
            IRepository<CustomerEntity, int> customerRepository,
            IRepository<OrderItemEntity> orderItemRepository,
            IRepository<PromotionEntity, int> promotionRepository,
            IRepository<AddressEntity, int> addressRepository,            
            IRepository<PaymentEntity, int> paymentRepository,            
            IOrderRepository orderRepository)
        {
            DeliveryScheduleAppService = deliveryScheduleAppService;
            DeliveryChargeAppService = deliveryChargeAppService;
            PromotionRepository = promotionRepository;
            AddressRepository = addressRepository;
            PaymentRepository = paymentRepository;
            OrderItemRepository = orderItemRepository;
            OrderRepository = orderRepository;
            ItemRepository = itemRepository;
            CustomerRepository = customerRepository;
            DeliveryChargeRepository = deliveryChargeRepository;
            RiderRepository = riderRepository;
        }

        public virtual async Task<OrderDto> GetAsync(int id)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            var role = CurrentUser.Roles[0];

            OrderEntity oE;

            if (role.ToLower() == RolesConsts.Customer.ToLower())
                oE = await OrderRepository.GetOrderForCustomerAsync(userId, id);
            else if (role.ToLower() == RolesConsts.Rider.ToLower())
                oE = await OrderRepository.GetOrderForRiderAsync(userId, id);
            else if (role.ToLower() == RolesConsts.Manager.ToLower())
                oE = await OrderRepository.GetOrderForManagerAsync(userId, id);
            else
                throw new BusinessException();

            _ = oE ?? throw new BusinessException();

            return ObjectMapper.Map<OrderEntity, OrderDto>(oE);
        }

        public virtual async Task<OrderDto> CreateAsync(OrderCreateDto dto)
        {
            var userId = CurrentUser.Id;
            var addressE = (await AsyncExecuter
                .SingleOrDefaultAsync(from addrR in AddressRepository
                                      join cusR in CustomerRepository on addrR.CustomerID equals cusR.Id
                                      where cusR.UserID == userId && addrR.Id == dto.AddressID
                                      select addrR)) ?? throw new BusinessException();
            var itemIds = dto.Items.Select(e => e.ItemID).ToArray();
            var dsEArr = await DeliveryScheduleAppService.GetAllCompatiblesAsync(addressE.Id, itemIds);
            var dsE = dsEArr.SingleOrDefault(e => e.Id == dto.DeliveryScheduleID) ?? throw new BusinessException();
            var promotionE = (dto.PromotionID != null) ? await GetPromotionEntity((int)dto.PromotionID) : null;
            var subtotal = 0.0;
            var orderItemEList = new List<OrderItemEntity>();

            if (!(await ItemRepository.ValidateItemsWithinRadiusAsync(addressE.CityID, itemIds)))
                throw new BusinessException();

            foreach(var cartItem in dto.Items)
            {
                var item = await ItemRepository.SingleOrDefaultAsync(e => e.Id == cartItem.ItemID) ?? throw new BusinessException();
                subtotal += item.ItemPrice * cartItem.Qty;
                orderItemEList.Add(new OrderItemEntity(0, item.Id, (uint)cartItem.Qty, item.ItemPrice));
            }

            var dcDto = await DeliveryChargeAppService.CalculateAsync(addressE.Id, dsE.Id, dto.Items.ToArray());

            var orderE = new OrderEntity(addressE.Id, dsE.Id, promotionE?.Id);

            orderE = await OrderRepository.InsertAsync(orderE, autoSave: true);

            foreach(var oi in orderItemEList)
            {                
                var item = new OrderItemEntity(orderE.Id, oi.ItemID, oi.Quantity, oi.ItemPrice);
                await OrderItemRepository.InsertAsync(item, autoSave: true);
            }

            var dCharge = dcDto.DistanceCharge + dcDto.SubtotalPercentage + dcDto.IncreasedCost;

            if (promotionE != null)
            {
                dCharge = dcDto.SubtotalPercentage;
                if (promotionE.IsOneTime || promotionE.NoOfTimes - 1 < 1)
                    await PromotionRepository.DeleteAsync(promotionE);
            }

            var dcE = new DeliveryChargeEntity(dCharge, dcDto.DistanceChargeID, dcDto.SubtotalPercentageID, dcDto.DeliveryScheduleID);
            
            await DeliveryChargeRepository.InsertAsync(dcE, autoSave: true);                        

            var paymentE = new PaymentEntity(orderE.Id, dcE.Id, (float)(subtotal + dcE.Charge), (float)subtotal);
            
            await PaymentRepository.InsertAsync(paymentE, autoSave: true);

            return ObjectMapper.Map<OrderEntity, OrderDto>(orderE);
        }

        protected async Task<PromotionEntity> GetPromotionEntity(int id)
        {
            var promotionE = await PromotionRepository.SingleOrDefaultAsync(e => e.Id == id) ?? throw new BusinessException();
            if (DateTime.Compare(promotionE.ExpireDate.ClearTime(), DateTime.Now.ClearTime()) <= 0)
                throw new BusinessException();

            if (!promotionE.IsOneTime && promotionE.NoOfTimes < 1)
                throw new BusinessException();

            return promotionE;
        }

        public virtual async Task<OrderDto[]> GetOrdersForManagerAsync(int cityId = 0, int skipCount = 0, int maxResultCount = 10)
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");

            var orders = await OrderRepository.GetOrdersForManagerAsync((Guid)userId, cityId, skipCount, maxResultCount);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetActiveOrdersForManagerAsync(int cityId = 0, int skipCount = 0, int maxResultCount = 10)
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");

            var orders = await OrderRepository.GetActiveOrdersForManagerAsync((Guid)userId, cityId, skipCount, maxResultCount);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetOrdersForRiderAsync(int skipCount = 0, int maxResultCount = 10)
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");                       

            var orders = await OrderRepository.GetOrdersForRiderAsync((Guid)userId, skipCount, maxResultCount);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetActiveOrdersForRiderAsync(int skipCount = 0, int maxResultCount = 10)
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");            

            try
            {
                _= await OrderRepository.GetActiveOrdersForRiderAsync((Guid)userId, skipCount, maxResultCount);
            }
            catch(Exception ex)
            {

            }

            var orders = await OrderRepository.GetActiveOrdersForRiderAsync((Guid)userId, skipCount, maxResultCount);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetSelectedOrdersForRiderAsync()
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");

            var orders = await OrderRepository.GetSelectedOrdersForRiderAsync((Guid)userId);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetActiveOrdersForCustomerAsync()
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");

            var orders = await OrderRepository.GetActiveOrdersForCustomerAsync((Guid)userId);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task<OrderDto[]> GetOrdersForCustomerAsync(int skipCount = 0, int maxResultCount = 10)
        {
            var userId = CurrentUser.Id;

            if (userId == null) throw new BusinessException("User must login first");

            var orders = await OrderRepository.GetOrdersForCustomerAsync((Guid)userId, skipCount, maxResultCount);

            return ObjectMapper.Map<OrderEntity[], OrderDto[]>(orders);
        }

        public virtual async Task SelectOrderAsync(int orderId)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            await OrderRepository.SelectOrderAsync((Guid)userId, orderId);
        }

        public virtual async Task DeselectOrderAsync(int orderId)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            await OrderRepository.DeselectOrderAsync(userId, orderId);
        }

        public virtual async Task MarkAsDeliveredOrderAsync(int orderId, PaymentDetailDto[] payments)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            var paymentEs = new List<PaymentDetailEntity>();

            foreach (var p in payments)
                paymentEs.Add(new PaymentDetailEntity(0, p.PaymentMethod, p.TotalPaid));

            await OrderRepository.MarkAsDeliveredOrderAsync(userId, orderId, paymentEs.ToArray());
        }        

        public virtual async Task<CancellationReasonDto[]> GetAllCancellationReasonAsync()
        {
            var crEs = await OrderRepository.GetAllCancellationReasonAsync();
            return ObjectMapper.Map<CancellationReasonEntity[], CancellationReasonDto[]>(crEs);
        }

        public virtual async Task<CancelledOrderDto> SendCancellationRequestAsync(CancelledOrderDto dto)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            var entity = ObjectMapper.Map<CancelledOrderDto, CancelledOrderEntity>(dto);
            
            var crE = await OrderRepository.SendCancellationRequestAsync(userId, entity);            

            return ObjectMapper.Map<CancelledOrderEntity, CancelledOrderDto>(crE);
        }

        public virtual async Task<OrderDto> RollbackCancellationRequestAsync(int orderId)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            var oE = await OrderRepository.RollbackCancellationRequestAsync(userId, orderId);
            return ObjectMapper.Map<OrderEntity, OrderDto>(oE);
        }

        public virtual async Task RemoveAsync(int id)
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            await OrderRepository.RemoveAsync(userId, id);
        }
    }
}
