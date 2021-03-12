using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account.Settings;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Settings;

namespace HDHDC.Speedwave.AccountAppServices
{
    public class CustomerAccountAppService : SpeedyAccountAppService, IApplicationService, ICustomerAccountAppService
    {
        protected IRepository<CustomerEntity, int> CustomerRepository { get; }

        public CustomerAccountAppService(
            IdentityUserManager userManager, 
            IIdentityRoleRepository roleRepository,
            IRepository<CustomerEntity, int> customerRepository)
            : base(userManager, roleRepository)
        {
            CustomerRepository = customerRepository;
        }

        public override async Task<AppUserDto> RegisterAsync(UserCreateDto input)
        {
            await CheckSelfRegistrationAsync();

            var userDto = await base.RegisterAsync(input);
            var user = await UserManager.FindByNameAsync(userDto.UserName);            

            await UserManager.AddToRoleAsync(user, RolesConsts.Customer);
            var customerDto = await CustomerRepository.InsertAsync(new CustomerEntity(user.Id) { Status = EntityStatusConsts.Active }, autoSave: true);

            await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("customerId", customerDto.Id.ToString()));

            return userDto;
        }

        protected virtual async Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }
    }
}
