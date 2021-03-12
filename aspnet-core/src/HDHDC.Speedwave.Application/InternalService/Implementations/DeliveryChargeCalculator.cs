using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.InternalService.Services;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.UtilityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace HDHDC.Speedwave.InternalService.Implementations
{
    internal class DeliveryChargeCalculator : IDeliveryChargeCalculator
    {
        public DeliveryChargeCalculator(
            IGeoDistanceCalculator geoDistanceCalculator, 
            IRepository<AddressEntity, int> addressRepository, 
            IRepository<DistanceChargeEntity, int> distanceChargeRepository, 
            IAsyncQueryableExecuter asyncExecutor, 
            IRiderRepository riderRepository, 
            IRepository<ItemEntity, int> itemRepository, 
            IRepository<DeliveryScheduleEntity, int> deliveryScheduleRepository, 
            IRepository<SubtotalPercentageEntity, int> subtotalPercentageRepository)
        {
            GeoDistanceCalculator = geoDistanceCalculator;
            AddressRepository = addressRepository;
            DistanceChargeRepository = distanceChargeRepository;
            AsyncExecutor = asyncExecutor;
            RiderRepository = riderRepository;
        }

        protected IGeoDistanceCalculator GeoDistanceCalculator { get; }
        protected IRepository<AddressEntity, int> AddressRepository { get; }
        protected IRepository<DistanceChargeEntity, int> DistanceChargeRepository { get; }
        protected IAsyncQueryableExecuter AsyncExecutor { get; }
        protected IRiderRepository RiderRepository { get; }
        protected IRepository<ItemEntity, int> ItemRepository { get; }
        protected IRepository<DeliveryScheduleEntity, int> DeliveryScheduleRepository { get; }
        protected IRepository<SubtotalPercentageEntity, int> SubtotalPercentageRepository { get; }
        public virtual async Task<DeliveryChargeEntity> CalculateAsync(int addressId, int deliveryScheduleId, float subtotal, bool hasPromotion = false)
        {
            var addrE = await AddressRepository.FindAsync(addressId) ?? throw new EntityNotFoundException();
            var dScheduleE = await DeliveryScheduleRepository.FindAsync(deliveryScheduleId) ?? throw new EntityNotFoundException();
            var riderEs = await RiderRepository.GetRidersForTheCity(addrE.CityID);
            var meanDistance = GetMeanDistance(addrE, riderEs);
            var distanceChargeE = await AsyncExecutor.LastOrDefaultAsync(DistanceChargeRepository
                .Where(e => e.From < meanDistance)
                .OrderBy(e => e.From));            

            var subtotalPercentageE = await AsyncExecutor.LastOrDefaultAsync(SubtotalPercentageRepository.Where(e => e.From < subtotal)
                .OrderBy(e => e.From));

            var charge = subtotal * subtotalPercentageE.Percentage;
            charge += (hasPromotion)? 0 : distanceChargeE.Charge;

            charge += charge * dScheduleE.CostIncreasePercentage;

            return new DeliveryChargeEntity(charge, distanceChargeE.Id, subtotalPercentageE.Id, dScheduleE.Id);
        }
        private double GetMeanDistance(AddressEntity addrE, IList<RiderEntity> riderEs)
        {
            if (addrE == null || riderEs.Count() == 0)
                throw new ArgumentException();

            var point01 = GetPoint(addrE.Geolocation);
            var total = 0.0;

            foreach (var riderE in riderEs)
                total += GeoDistanceCalculator.Calculate(point01, GetPoint(riderE.Geolocation));

            return total / riderEs.Count();
        }

        private Tuple<double, double> GetPoint(string geolocation)
        {
            var latLonArr = geolocation.Split(':');

            if (latLonArr.Length != 2 || !double.TryParse(latLonArr[0], out var lat) || !double.TryParse(latLonArr[1], out var lon))
                throw new BusinessException();

            return Tuple.Create(lat, lon);
        }
    }
}
