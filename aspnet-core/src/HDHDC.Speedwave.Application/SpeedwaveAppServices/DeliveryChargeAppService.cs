using HDHDC.Speedwave.Dependencies;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.InternalService.Services;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class DeliveryChargeAppService : ApplicationService, IDeliveryChargeAppService
    {
        protected IDeliveryChargeRepository DeliveryChargeRepository { get; }
        protected IRepository<SubtotalPercentageEntity, int> SubtotalPercentageRepository { get; }
        protected IRepository<ItemEntity, int> ItemRepository { get; }
        protected IDeliveryScheduleRepository DeliveryScheduleRepository { get; }
        protected IRepository<DistanceChargeEntity> DistanceChargeRepository { get; }

        public DeliveryChargeAppService(
            IDeliveryChargeRepository deliveryChargeRepository,
            IDeliveryScheduleRepository deliveryScheduleRepository,
            IRepository<ItemEntity, int> itemRepository,
            IRepository<SubtotalPercentageEntity, int> subtotalPercentageRepository,
            IRepository<DistanceChargeEntity> distanceChargeRepository)
        {
            DeliveryChargeRepository = deliveryChargeRepository;
            SubtotalPercentageRepository = subtotalPercentageRepository;
            ItemRepository = itemRepository;
            DeliveryScheduleRepository = deliveryScheduleRepository;
            DistanceChargeRepository = distanceChargeRepository;
        }

        public virtual async Task<DeliveryChargeDto> CalculateAsync(int addressId, int deliveryScheduleId, CartItemDto[] items)
        {                       
            var meanDistance = await DeliveryChargeRepository.GetMeanDistanceAsync(addressId);
            var distanceChargeE = await AsyncExecuter
                .LastOrDefaultAsync(from dcR in DistanceChargeRepository
                                    orderby dcR.From ascending
                                    where dcR.From <= meanDistance.MeanDistance
                                    select dcR);
            _= distanceChargeE ?? throw new BusinessException();
            var subtotal = 0.0;

            foreach(var ci in items)
            {
                var itemE = await ItemRepository.SingleOrDefaultAsync(e => e.Id == ci.ItemID)
                    ?? throw new BusinessException();
                subtotal += itemE.ItemPrice * ci.Qty;
            }

            var subtotalPerE = await AsyncExecuter
                .LastOrDefaultAsync(from subPR in SubtotalPercentageRepository
                                    orderby subPR.From ascending
                                    where subPR.From <= subtotal
                                    select subPR);


            var itemIdsArr = items.Select(e => e.ItemID).ToArray();
            var dsArr = await DeliveryScheduleRepository.GetCompatibleSchedules((new Guid()), addressId, itemIdsArr);
            var dsE = dsArr.SingleOrDefault(e => e.Id == deliveryScheduleId) ?? throw new BusinessException();

            float dsCost = (float)(subtotal * (dsE.CostIncreasePercentage/100));
            var subPer = (subtotalPerE == null)? 0 : (subtotal * ((float)subtotalPerE.Percentage / 100));

            return new DeliveryChargeDto
            {
                DistanceChargeID = distanceChargeE.Id,
                DeliveryScheduleID = dsE.Id,
                SubtotalPercentageID = (subtotalPerE == null)? 0 : subtotalPerE.Id,
                DistanceCharge = distanceChargeE.Charge,
                IncreasedCost = dsCost,
                SubtotalPercentage = (float)subPer,
                Subtotal = (float)subtotal,
                Nettotal = (float)(subtotal + distanceChargeE.Charge + dsCost + subPer)
            };
        }
    }
}
