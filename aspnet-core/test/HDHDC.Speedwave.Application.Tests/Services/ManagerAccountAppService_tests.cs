using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedy.SpeedyAppServices;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Xunit;

namespace HDHDC.Speedwave.Services
{
    public class ManagerAccountAppService_tests : SpeedwaveApplicationTestBase
    {
        protected ManagerAccountAppService ManagerAccountAppService { get; }
        protected IIdentityUserAppService IdentityUserAppService { get; }
        protected ManagerAppService ManagerAppService { get; }
        protected IDistrictAppService DistrictAppService { get; }
        protected IProvinceAppService ProvinceAppService { get; }

        public ManagerAccountAppService_tests()
        {
            ManagerAccountAppService = GetRequiredService<ManagerAccountAppService>();
            IdentityUserAppService = GetRequiredService<IIdentityUserAppService>();
            ManagerAppService = GetRequiredService<ManagerAppService>();
            DistrictAppService = GetRequiredService<IDistrictAppService>();
            ProvinceAppService = GetRequiredService<IProvinceAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Manager()
        {            
            var userCreateDto = new UserCreateDto
            {
                Name = "ManagerOne",
                Surname = "Manager",
                EmailAddress = "ManagerOne@test.com",
                PhoneNumber = "0760000000",
                UserName = "ManagerOne",
                Password = "1qaz@WSX"
            };

            var districtId = "Colombo";

            await WithUnitOfWorkAsync(async () =>
            {
                await ManagerAccountAppService.RegisterAsync(userCreateDto, districtId);
            });

            var userDto = await IdentityUserAppService.FindByUsernameAsync(userCreateDto.UserName);

            userDto.ShouldNotBeNull();

            userDto.Name.ShouldBe(userCreateDto.Name);
            userDto.Surname.ShouldBe(userCreateDto.Surname);
            userDto.Email.ShouldBe(userCreateDto.EmailAddress);
            userDto.PhoneNumber.ShouldBe(userCreateDto.PhoneNumber);

            var roles = (await IdentityUserAppService.GetRolesAsync(userDto.Id)).Items;

            roles.ShouldContain(role => role.Name == RolesConsts.Manager);

          //  var managerDto = await ManagerAppService.GetManagerByUserId(userDto.Id);

            //managerDto.ShouldNotBeNull();
            //managerDto.Status.ShouldBe(EntityStatusConsts.Active);
            //managerDto.DistrictID.ShouldBe(districtId);
        }
    }
}
