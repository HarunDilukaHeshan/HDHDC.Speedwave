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
    public class RiderAccountAppService : SpeedyAccountAppService, IApplicationService, IRiderAccountAppService
    {
        protected IRepository<RiderEntity, int> RiderRepository { get; }
        protected IRepository<CityEntity, int> CityRepository { get; }

        public RiderAccountAppService(
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IRepository<RiderEntity, int> riderRepository, 
            IRepository<CityEntity, int> cityRepository)
            : base(userManager, roleRepository)
        {
            RiderRepository = riderRepository;
            CityRepository = cityRepository;
        }

        [NonAction]
        public override Task<AppUserDto> RegisterAsync(UserCreateDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUserDto> RegisterAsync(UserCreateDto input, RiderCreateDto riderCreateDto)
        {

            var userDto = await base.RegisterAsync(input);
            var userE = await UserManager.FindByNameAsync(userDto.UserName);

            await UserManager.AddToRoleAsync(userE, RolesConsts.Rider);

            var cityDto = await CityRepository.FindAsync(riderCreateDto.CityID) ?? throw new BusinessException("Invalid city");

            await RiderRepository.InsertAsync(new RiderEntity(userDto.Id, riderCreateDto.Geolocation, cityDto.Id), autoSave: true);

            return userDto;
        }

    }
}
