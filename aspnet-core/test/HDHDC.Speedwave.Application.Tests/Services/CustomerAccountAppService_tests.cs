using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Xunit;
using Shouldly;
using HDHDC.Speedy.SpeedyAppServices;
using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.SpeedwaveAppServices;

namespace HDHDC.Speedwave.Services
{
    public class CustomerAccountAppService_tests : SpeedwaveApplicationTestBase
    {
        protected CustomerAccountAppService CustomerAccountAppService { get; }
        protected IIdentityUserAppService IdentityUserAppService { get; }
        protected ICustomerAppService CustomerAppService { get; }

        public CustomerAccountAppService_tests()
        {
            CustomerAccountAppService = GetRequiredService<CustomerAccountAppService>();
            IdentityUserAppService = GetRequiredService<IIdentityUserAppService>();
            CustomerAppService = GetRequiredService<ICustomerAppService>();
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_A_Customer()
        {
            var userCreateDto = new UserCreateDto
            { 
                 Name = "CustomerOne",
                 Surname = "Consumer",
                 EmailAddress = "CustomerOne@test.com",
                 PhoneNumber = "0760000000",
                 UserName = "CustomerOne",
                 Password = "1qaz@WSX"
            };

            await CustomerAccountAppService.RegisterAsync(userCreateDto);

            var userDto = await IdentityUserAppService.FindByUsernameAsync(userCreateDto.UserName);

            userDto.ShouldNotBeNull();

            userDto.Name.ShouldBe(userCreateDto.Name);
            userDto.Surname.ShouldBe(userCreateDto.Surname);
            userDto.Email.ShouldBe(userCreateDto.EmailAddress);
            userDto.PhoneNumber.ShouldBe(userCreateDto.PhoneNumber);

            var roles = (await IdentityUserAppService.GetRolesAsync(userDto.Id)).Items;

            roles.ShouldContain(role => role.Name == RolesConsts.Customer);

            var customerDto = await CustomerAppService.GetCustomerByUserId(userDto.Id);

            customerDto.ShouldNotBeNull("CustomerEntity must not be null");
            customerDto.Status.ShouldBe(EntityStatusConsts.Active);
        }
    }
}
