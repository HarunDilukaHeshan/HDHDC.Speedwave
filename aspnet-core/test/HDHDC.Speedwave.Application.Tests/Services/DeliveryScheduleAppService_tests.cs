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
    public class DeliveryScheduleAppService_tests : SpeedwaveApplicationTestBase
    {
        protected IDeliveryScheduleAppService DeliveryScheduleAppService { get; }
        public DeliveryScheduleAppService_tests()
        {
            DeliveryScheduleAppService = GetRequiredService<IDeliveryScheduleAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Delivery_Schedule()
        {
            var dScheduleCreateDto = new DeliveryScheduleCreateUpdateDto
            {
                DeliveryScheduleName = "Urgent",
                TimePeriod = new TimeSpan(2, 0, 0),
                CostIncreasePercentage = 10/100
            };

            var dScheduleDto = await DeliveryScheduleAppService.CreateAsync(dScheduleCreateDto);

            dScheduleDto.ShouldNotBeNull();

            dScheduleDto.Id.ShouldBeGreaterThan(0);
            dScheduleDto.DeliveryScheduleName.ShouldBe(dScheduleCreateDto.DeliveryScheduleName);
            dScheduleDto.TimePeriod.ShouldBe(dScheduleCreateDto.TimePeriod);
            dScheduleDto.CostIncreasePercentage.ShouldBe(dScheduleCreateDto.CostIncreasePercentage);
        }
    }
}
