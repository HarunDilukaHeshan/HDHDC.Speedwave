using HDHDC.Speedwave.SpeedwaveAppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using HDHDC.Speedwave.DTOs;

namespace HDHDC.Speedwave.Services
{
    public class DistrictAppService_tests : SpeedwaveApplicationTestBase
    {
        protected IDistrictAppService DistrictAppService { get; }
        public DistrictAppService_tests()
        {
            DistrictAppService = GetRequiredService<IDistrictAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_District()
        {
            var districtCreateDto = new DistrictCreateDto
            {
                Id = "Colombo Fun",
                ProvinceID = "Western"
            };

            var districtDto = await DistrictAppService.CreateAsync(districtCreateDto);

            districtDto.ShouldNotBeNull();
            districtDto.Id.ShouldNotBeNullOrEmpty();
            districtDto.Id.ShouldNotBeNullOrWhiteSpace();
            districtDto.Id.ShouldBe(districtCreateDto.Id);
            districtDto.ProvinceID.ShouldBe(districtCreateDto.ProvinceID);
        }
    }
}
