using HDHDC.Speedwave.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.AccountAppServices
{
    public class SpeedyAccountAppService : ApplicationService, IApplicationService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityRoleRepository RoleRepository { get; }

        public SpeedyAccountAppService(IdentityUserManager userManager, IIdentityRoleRepository roleRepository)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
        }

        public virtual async Task<AppUserDto> RegisterAsync(UserCreateDto input)
        {
            //await CheckSelfRegistrationAsync();

            var user = new Volo.Abp.Identity.IdentityUser(GuidGenerator.Create(), input.UserName, input.EmailAddress, CurrentTenant.Id)
            {
                Name = input.Name,
                Surname = input.Surname
            };

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

            await UserManager.SetEmailAsync(user, input.EmailAddress);
            await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber);

            await UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("sub", user.Id.ToString()));            

            return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, AppUserDto>(user);
        }

        //protected virtual async Task CheckSelfRegistrationAsync()
        //{
        //    if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled))
        //    {
        //        throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
        //    }
        //}

        public virtual async Task<AppUserDto> UpdateUserAsync(Guid id, UserUpdateDto input)
        {
            var userE = await UserManager.GetByIdAsync(id) ?? throw new EntityNotFoundException();
            ObjectMapper.Map(input, userE);
            await UserManager.UpdateAsync(userE);

            return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, AppUserDto>(userE);
        }
    }
}
