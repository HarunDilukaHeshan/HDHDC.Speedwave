using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.UtilityServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Dependencies
{
    public class CitySelector : ITransientDependency
    {
        public CitySelector(
            IRepository<CityEntity, int> cityRepository,
            IGeoDistanceCalculator distanceCalculator, 
            IConfiguration config)
        {
            DistanceCalculator = distanceCalculator;
            Config = config;
            CityRepository = cityRepository;
        }

        protected IGeoDistanceCalculator DistanceCalculator { get; }
        protected IConfiguration Config { get; }
        protected IRepository<CityEntity, int> CityRepository { get; }

        public virtual async Task<IList<CityEntity>> GetCitiesAsync(int cityId)
        {
            var cityE = await CityRepository.FindAsync(cityId) ?? throw new EntityNotFoundException();
            var radius = Config.GetValue<float>("GeoDistance:MaxRadius");
            if (radius <= 0) throw new BusinessException();

            var point01 = GetPoint(cityE.Geolocation);
            var selectedList = new List<CityEntity>();
            var cityEsList = await CityRepository.GetListAsync();

            foreach(var cityEntity in cityEsList)
            {
                var point02 = GetPoint(cityEntity.Geolocation);
                var distance = DistanceCalculator.Calculate(point01, point02);
                if (distance <= radius) selectedList.Add(cityEntity);
            }

            return selectedList;
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
