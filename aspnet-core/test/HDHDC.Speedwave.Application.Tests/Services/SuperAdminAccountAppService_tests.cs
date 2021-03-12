using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
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
    public class SuperAdminAccountAppService_tests : SpeedwaveApplicationTestBase
    {
        protected SuperAdminAccountAppService SuperAdminAccountAppService { get; }
        protected IIdentityUserAppService IdentityUserAppService { get; }        

        public SuperAdminAccountAppService_tests()
        {
            SuperAdminAccountAppService = GetRequiredService<SuperAdminAccountAppService>();
            IdentityUserAppService = GetRequiredService<IIdentityUserAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Super_Admin()
        {
            var userCreateDto = new UserCreateDto
            {
                Name = "SuperAdminOne",
                Surname = "SuperAdmin",
                EmailAddress = "SuperAdminOne@test.com",
                PhoneNumber = "0760000000",
                UserName = "SuperAdminOne",
                Password = "1qaz@WSX"
            };

            await SuperAdminAccountAppService.RegisterAsync(userCreateDto);

            var userDto = await IdentityUserAppService.FindByUsernameAsync(userCreateDto.UserName);            

            userDto.ShouldNotBeNull();

            var roles = (await IdentityUserAppService.GetRolesAsync(userDto.Id)).Items;

            roles.ShouldContain(role => role.Name == RolesConsts.SuperAdmin);

            userDto.Name.ShouldBe(userCreateDto.Name);
            userDto.Surname.ShouldBe(userCreateDto.Surname);
            userDto.Email.ShouldBe(userCreateDto.EmailAddress);
            userDto.PhoneNumber.ShouldBe(userCreateDto.PhoneNumber);            
        }
    }
}
