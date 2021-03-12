using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HDHDC.Speedwave.Services
{
    public class DistanceChargeAppService_tests : SpeedwaveApplicationTestBase
    {
        protected IDistanceChargeAppService DistanceChargeAppService { get; }
        public DistanceChargeAppService_tests()
        {
            DistanceChargeAppService = GetRequiredService<IDistanceChargeAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Distance_Charge()
        {
            var distanceChargeCreateDto = new DistanceChargeCreateUpdateDto
            {
                From = 0,
                Charge = 200
            };

            var distanceChargeDto = await DistanceChargeAppService.CreateAsync(distanceChargeCreateDto);

            distanceChargeDto.ShouldNotBeNull();
            distanceChargeDto.Id.ShouldBeGreaterThan(0);
            distanceChargeDto.From.ShouldBe(distanceChargeCreateDto.From);
            distanceChargeDto.Charge.ShouldBe(distanceChargeCreateDto.Charge);
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_The_Second_Distance_Charge()
        {
            var firstDChargeCreateDto = new DistanceChargeCreateUpdateDto
            {
                From = 0,
                Charge = 150
            };

            await DistanceChargeAppService.CreateAsync(firstDChargeCreateDto);

            var distanceChargeCreateDto = new DistanceChargeCreateUpdateDto
            {
                From = 4,
                Charge = 300
            };

            var distanceChargeDto = await DistanceChargeAppService.CreateAsync(distanceChargeCreateDto);

            distanceChargeDto.ShouldNotBeNull();
            distanceChargeDto.Id.ShouldBeGreaterThan(0);
            distanceChargeDto.From.ShouldBe(distanceChargeCreateDto.From);
            distanceChargeDto.Charge.ShouldBe(distanceChargeCreateDto.Charge);
        }

        [Fact]
        public async Task Should_Be_Able_To_Update_A_Distance_Charge()
        {
            var distanceChargeCreateDto = new DistanceChargeCreateUpdateDto
            {
                From = 0,
                Charge = 200
            };

            var distanceChargeDto = await DistanceChargeAppService.CreateAsync(distanceChargeCreateDto);

            var distanceChargeUpdateDto = new DistanceChargeCreateUpdateDto
            {
                From = 0,
                Charge = 300
            };

            var distanceChargeUpdatedDto = await DistanceChargeAppService.UpdateAsync(distanceChargeDto.Id, distanceChargeUpdateDto);

            distanceChargeUpdatedDto.ShouldNotBeNull();
            distanceChargeUpdatedDto.Id.ShouldBeGreaterThan(0);
            distanceChargeUpdatedDto.Id.ShouldNotBe(distanceChargeDto.Id);
            distanceChargeUpdatedDto.From.ShouldBe(distanceChargeUpdateDto.From);
            distanceChargeUpdatedDto.Charge.ShouldBe(distanceChargeUpdateDto.Charge);
        }        
    }
}
