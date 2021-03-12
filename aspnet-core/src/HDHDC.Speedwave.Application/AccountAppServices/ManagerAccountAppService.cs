
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.AccountAppServices
{
    public class ManagerAccountAppService : SpeedyAccountAppService, 
        IApplicationService,
        IManagerAccountAppService
    {
        protected IRepository<ManagerEntity, int> ManagerRepository { get; }

        public ManagerAccountAppService(
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IRepository<ManagerEntity, int> managerRepository)
            : base(userManager, roleRepository)
        {
            ManagerRepository = managerRepository;
        }

        [NonAction]
        public override Task<AppUserDto> RegisterAsync(UserCreateDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUserDto> RegisterAsync(UserCreateDto input, string districtID)
        {

            var userDto = await base.RegisterAsync(input);

            var user = await UserManager.FindByNameAsync(userDto.UserName);

            await UserManager.AddToRoleAsync(user, RolesConsts.Manager);
            await ManagerRepository.InsertAsync(new ManagerEntity(user.Id, districtID) { Status = EntityStatusConsts.Active }, autoSave: true);

            return userDto;
        }
    }
}
