using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedyConsts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Xunit;

namespace HDHDC.Speedwave.Services
{
    public class RiderAccountAppService_tests : SpeedwaveApplicationTestBase
    {
        protected RiderAccountAppService RiderAccountAppService { get; }
        protected IIdentityUserAppService IdentityUserAppService { get; }      
        protected IRiderAppService RiderAppService { get; }
        public IProvinceAppService ProvinceAppService { get; }
        public IDistrictAppService DistrictAppService { get; }
        public ICityAppService CityAppService { get; }

        public RiderAccountAppService_tests()
        {
            RiderAccountAppService = GetRequiredService<RiderAccountAppService>();
            IdentityUserAppService = GetRequiredService<IIdentityUserAppService>();
            RiderAppService = GetRequiredService<IRiderAppService>();
            ProvinceAppService = GetRequiredService<IProvinceAppService>();
            DistrictAppService = GetRequiredService<IDistrictAppService>();
            CityAppService = GetRequiredService<ICityAppService>();

        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Rider()
        {
            var districtId = "Colombo";
            var cityCreateDto = new CityCreateDto
            {
                CityName = "Delkanda",
                DistrictID = districtId,
                Geolocation = "6.784568:79.546545"
            };

            var cityDto = await CityAppService.CreateAsync(cityCreateDto);

            var userCreateDto = new UserCreateDto
            {
                Name = "RiderOne",
                Surname = "Rider",
                EmailAddress = "RiderOne@test.com",
                PhoneNumber = "0760000000",
                UserName = "RiderOne",
                Password = "1qaz@WSX"
            };

            await WithUnitOfWorkAsync(async () =>
            {
                //await RiderAccountAppService.RegisterAsync(userCreateDto, cityDto.Id);
            });            

            var userDto = await IdentityUserAppService.FindByUsernameAsync(userCreateDto.UserName);

            userDto.ShouldNotBeNull();

            userDto.Name.ShouldBe(userCreateDto.Name);
            userDto.Surname.ShouldBe(userCreateDto.Surname);
            userDto.Email.ShouldBe(userCreateDto.EmailAddress);
            userDto.PhoneNumber.ShouldBe(userCreateDto.PhoneNumber);

            var roles = (await IdentityUserAppService.GetRolesAsync(userDto.Id)).Items;

            roles.ShouldContain(role => role.Name == RolesConsts.Rider);

           // var riderDto = await RiderAppService.GetRiderByUserId(userDto.Id);

            //riderDto.ShouldNotBeNull();
            //riderDto.Status.ShouldBe(EntityStatusConsts.Active);
            //riderDto.Geolocation.ShouldBe(cityCreateDto.Geolocation);
        }
    }
}
