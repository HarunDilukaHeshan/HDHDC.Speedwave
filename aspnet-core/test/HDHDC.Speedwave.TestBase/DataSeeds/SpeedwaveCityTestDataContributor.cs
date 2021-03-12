using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.DataSeeds
{
    public class SpeedwaveCityTestDataContributor : IDataSeedContributor, ITransientDependency
    {
        public SpeedwaveCityTestDataContributor(
            IRepository<CityEntity, int> cityRepo, 
            IRepository<DistrictEntity, string> districtRepo, 
            IRepository<ProvinceEntity, string> provinceRepo)
        {
            CityRepo = cityRepo;
            DistrictRepo = districtRepo;
            ProvinceRepo = provinceRepo;
        }

        public IRepository<CityEntity, int> CityRepo { get; }
        public IRepository<DistrictEntity, string> DistrictRepo { get; }
        public IRepository<ProvinceEntity, string> ProvinceRepo { get; }

        public async Task SeedAsync(DataSeedContext context)
        {
            var provinceE = new ProvinceEntity("Western");
            var districtE = new DistrictEntity("Colombo", provinceE.Id);
            var cityE = new CityEntity(districtE.Id)
            {
                CityName = "Delkanda",
                Geolocation = "6.784568:79.546545"
            };

            await ProvinceRepo.InsertAsync(provinceE);
            await DistrictRepo.InsertAsync(districtE);
            await CityRepo.InsertAsync(cityE);
        }
    }
}
