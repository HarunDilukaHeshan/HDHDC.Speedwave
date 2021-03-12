using HDHDC.Speedwave.DTOs;
using HDHDC.Speedy.SpeedyAppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Xunit;
using Shouldly;

namespace HDHDC.Speedwave.Services
{
    public class AddressAppService_tests : SpeedwaveApplicationTestBase
    {
        protected AddressAppService AddressAppService { get; }

        public AddressAppService_tests()
        {
            AddressAppService = GetRequiredService<AddressAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_An_Address()
        {
            var addressCreateDto = new AddressCreateDto
            {
                 AddressLine = "95/2T, Testing road, Xunit, Sri Lanka",
                 CityID = 1,
                 CustomerID = 1,
                 Geolocation = "6.789456:79.456123",
                 Note = "The road to the left of the grocery store at the Xunit junction"
            };
           
            var addressDto = await AddressAppService.CreateAsync(addressCreateDto);

            addressDto.Id.ShouldBeGreaterThan(0);

            addressDto.AddressLine.ShouldBe(addressCreateDto.AddressLine);
            addressDto.CityID.ShouldBe(1);
            addressDto.CustomerID.ShouldBe(1);
            addressDto.Geolocation.ShouldBe(addressCreateDto.Geolocation);
            addressDto.Note.ShouldBe(addressCreateDto.Note);
        }
    }
}
