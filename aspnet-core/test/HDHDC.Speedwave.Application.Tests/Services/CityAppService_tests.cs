using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace HDHDC.Speedwave.Services
{
    public class CityAppService_tests : SpeedwaveApplicationTestBase
    {
        protected ICityAppService CityAppService { get; }
        public CityAppService_tests()
        {
            CityAppService = GetRequiredService<ICityAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_City()
        {
            var cityCreateDto = new CityCreateDto
            {
                 CityName = "Delkanda01",
                 DistrictID = "Colombo",
                 Geolocation = "6.789456:79.456133"
            };

            var cityDto = await CityAppService.CreateAsync(cityCreateDto);

            cityDto.ShouldNotBeNull();
            cityDto.Id.ShouldBeGreaterThan(0);
            cityDto.CityName.ShouldBe(cityCreateDto.CityName);
            cityDto.DistrictID.ShouldBe(cityCreateDto.DistrictID);
            cityDto.Geolocation.ShouldBe(cityCreateDto.Geolocation);
        }
    }
}
