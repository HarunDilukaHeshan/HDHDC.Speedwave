using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class ManagerAppService: ApplicationService, IManagerAppService
    {
        protected IManagerRepository Repository { get; }

        protected IIdentityUserRepository UserRepository { get; }

        protected IRepository<DistrictEntity, string> DistrictRepository { get; }

        public ManagerAppService(
            IIdentityUserRepository userRepository,
            IManagerRepository repository, 
            IRepository<DistrictEntity, string> districtRepository)
            : base()
        {
            Repository = repository;
            DistrictRepository = districtRepository;
            UserRepository = userRepository;
        }

        public async Task ChangeDistrictAsync(int managerId, DistrictDto districtDto)
        {
            if (managerId < 1 || string.IsNullOrWhiteSpace(districtDto.Id)) throw new ArgumentException();

            var managerE = await Repository.FindAsync(managerId) ?? throw new EntityNotFoundException("Manager does not exist");
            var districtE = await DistrictRepository.GetAsync(districtDto.Id) ?? throw new BusinessException("District does not exist");

            managerE.DistrictID = districtE.Id;

            await Repository.UpdateAsync(managerE);
        }

        public async Task BlockAsync(int managerId)
        {
            if (managerId < 1) throw new ArgumentException();
            var managerE = await Repository.FindAsync(managerId) ?? throw new EntityNotFoundException("Manager does not exist");

            managerE.Status = EntityStatusConsts.Blocked;
            await Repository.UpdateAsync(managerE);
        }

        public async Task WarnFirstAsync(int managerId)
        {
            if (managerId < 1) throw new ArgumentException();
            var managerE = await Repository.FindAsync(managerId) ?? throw new EntityNotFoundException("Manager does not exist");

            managerE.Status = EntityStatusConsts.WarningOne;
            await Repository.UpdateAsync(managerE);
        }

        public async Task WarnSecondAsync(int managerId)
        {
            if (managerId < 1) throw new ArgumentException();
            var managerE = await Repository.FindAsync(managerId) ?? throw new EntityNotFoundException("Manager does not exist");

            managerE.Status = EntityStatusConsts.WarningTwo;
            await Repository.UpdateAsync(managerE);
        }

        public async Task UnblockAsync(int managerId)
        {
            if (managerId < 1) throw new ArgumentException();
            await WarnFirstAsync(managerId);
        }

        public async Task<ManagerDto> GetManagerByUserIdAsync(Guid userId)
        {
            var userE = await UserRepository.GetAsync(userId) ?? throw new BusinessException("Invalid userId");
            var managerE = await Repository.GetAsync(e => e.UserID == userE.Id);

            return ObjectMapper.Map<ManagerEntity, ManagerDto>(managerE);
        }

        public async Task<ManagerDto[]> GetAllManagerByDistrictIdAsync(string districtId, bool includeDetails = false)
        {
            _ = await DistrictRepository.GetAsync(districtId) ?? throw new BusinessException("Invalid district");
            var managerEArr = await Repository.GetByDistrictIdAsync(districtId, includeDetails);

            return ObjectMapper.Map<ManagerEntity[], ManagerDto[]>(managerEArr);
        }

        public async Task<ManagerDto[]> GetAllAsync(bool includeDetails = false)
        {                     
            var managerEArr = await Repository.GetArrayAsync(includeDetails);

            return ObjectMapper.Map<ManagerEntity[], ManagerDto[]>(managerEArr);
        }

        public async Task<ManagerDto> GetAsync(int id)
        {
            var managerE = await Repository.GetAsync(id);
            return ObjectMapper.Map<ManagerEntity, ManagerDto>(managerE);
        }
    }
}
