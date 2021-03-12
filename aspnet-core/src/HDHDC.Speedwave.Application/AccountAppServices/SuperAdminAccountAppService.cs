using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedyConsts;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.AccountAppServices
{
    public class SuperAdminAccountAppService : SpeedyAccountAppService, IApplicationService
    {
        public SuperAdminAccountAppService(
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository)
            : base(userManager, roleRepository)
        { }

        public override async Task<AppUserDto> RegisterAsync(UserCreateDto input)
        {
            var userDto = await base.RegisterAsync(input);
            var user = await UserManager.FindByNameAsync(userDto.UserName);
            await UserManager.AddToRoleAsync(user, RolesConsts.SuperAdmin);

            return userDto;
        }
    }
}
