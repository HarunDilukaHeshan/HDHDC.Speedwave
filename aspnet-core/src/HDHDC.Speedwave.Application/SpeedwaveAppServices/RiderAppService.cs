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

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class RiderAppService : ApplicationService, IRiderAppService
    { 
        protected IRiderRepository Repository { get; }
        protected ICityRepository CityRepository { get; }

        protected IRiderCoverageRepository RiderCoverageRepository { get; }

        public RiderAppService(
            IRiderCoverageRepository riderCoverageRepository,
            ICityRepository cityRepository,
            IRiderRepository repository)
        {
            Repository = repository;
            CityRepository = cityRepository;
            RiderCoverageRepository = riderCoverageRepository;
        }

        public async Task<RiderDto> GetCurrentRiderAsync()
        {
            var userId = CurrentUser.Id;
            var userE = await Repository.GetAsync(e => e.UserID == userId) ?? throw new BusinessException("User must signin first");
            var cityE = await CityRepository.GetAsync(userE.CityID) ?? throw new BusinessException();

            userE.CityEntity = cityE;

            return ObjectMapper.Map<RiderEntity, RiderDto>(userE);
        }

        public async Task<CityDto[]> GetRiderCoverageAsync()
        {
            var userId = CurrentUser.Id;
            var riderE = await Repository.GetAsync(e => e.UserID == userId) ?? throw new BusinessException("User must signin first");
            var cityEArr = await AsyncExecuter.ToArrayAsync(from cityR in CityRepository
                                                            join riderCR in RiderCoverageRepository on cityR.Id equals riderCR.CityID
                                                            where riderCR.RiderID == riderE.Id && riderE.Status != EntityStatusConsts.Blocked
                                                            select cityR);

            return ObjectMapper.Map<CityEntity[], CityDto[]>(cityEArr);
        }

        public async Task<CityDto[]> UpdateRiderCoverageAsync(int[] cityIds)
        {
            var userId = CurrentUser.Id;
            var riderE = await Repository.GetAsync(e => e.UserID == userId) ?? throw new BusinessException();
            var cityEArr = new List<CityEntity>();
            var rcEArr = await AsyncExecuter.ToArrayAsync(RiderCoverageRepository.Where(e => e.RiderID == riderE.Id));            

            foreach(var id in cityIds)
            {
                var e = await CityRepository.GetAsync(id) ?? throw new BusinessException("Invalid id");
                cityEArr.Add(e);
            }

            var newCArr = cityEArr.Where(e => rcEArr.FirstOrDefault(ee => ee.CityID == e.Id) == null)
                .ToArray();

            var removed = rcEArr.Where(e => cityEArr.FirstOrDefault(ee => ee.Id == e.CityID) == null)
                .ToArray();

            foreach (var nCR in newCArr)
            {                
                await RiderCoverageRepository.InsertAsync(new RiderCoverageEntity(riderE.Id, nCR.Id), autoSave: true);
            }

            foreach (var rCR in removed)
                await RiderCoverageRepository.DeleteAsync(e => e.RiderID == riderE.Id && e.CityID == rCR.CityID, autoSave: true);            

            var riderCEArr = await AsyncExecuter.ToArrayAsync(from riderCR in RiderCoverageRepository
                                                        join cityR in CityRepository on riderCR.CityID equals cityR.Id
                                                        select cityR);

            return ObjectMapper.Map<CityEntity[], CityDto[]>(riderCEArr);
        }

        public async Task<RiderDto> GetAsync(int id)
        {
            var entity = await Repository.GetAsync(id);
            return ObjectMapper.Map<RiderEntity, RiderDto>(entity);
        }

        public async Task<RiderDto[]> GetListAsync(PagedAndSortedResultRequestDto input, bool includeDetails)
        {            
            var riderEs = await Repository.GetListAsync(input.SkipCount, input.MaxResultCount, includeDetails);
            return ObjectMapper.Map<RiderEntity[], RiderDto[]>(riderEs.ToArray());
        }

        public async Task<RiderDto[]> GetListAsync(int cityId, PagedAndSortedResultRequestDto input, bool includeDetails)
        {
            var riderEs = await Repository.GetListAsync(cityId, input.SkipCount, input.MaxResultCount, includeDetails);
            return ObjectMapper.Map<RiderEntity[], RiderDto[]>(riderEs.ToArray());
        }

        public async Task<RiderDto[]> SearchAsync(string words, int skipCount = 0, int maxResultCount = 10)
        {
            // Following line must be replaced to get the district Id from the current user who is a manager
            var districtId = "Colombo greater";
            var riderEs = await Repository.SearchAsync(words, districtId, skipCount, maxResultCount);
            return ObjectMapper.Map<RiderEntity[], RiderDto[]>(riderEs.ToArray());
        }

        public async Task<RiderDto> GetRiderByUserIdAsync (Guid userId)
        {
            var riderE = await Repository.FindAsync(e => e.UserID == userId);
            var riderDto = new RiderDto();

            if (riderE == null) return null;

            ObjectMapper.Map(riderE, riderDto);

            return riderDto;
        }

        public async Task<RiderDto> UpdateAsync(int id, RiderUpdateDto dto)
        {
            var entity = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();
            ObjectMapper.Map(dto, entity);
            try
            {
                await Repository.UpdateAsync(entity, autoSave: true);
            }
            catch(Exception ex) { }
            return ObjectMapper.Map<RiderEntity, RiderDto>(entity);
        }

        public async Task BlockAsync(int riderId)
        {
            if (riderId < 1) throw new ArgumentException();
            var riderE = await Repository.FindAsync(riderId) ?? throw new EntityNotFoundException("Manager does not exist");

            riderE.Status = EntityStatusConsts.Blocked;
            await Repository.UpdateAsync(riderE);
        }

        public async Task WarnFirstAsync(int riderId)
        {
            if (riderId < 1) throw new ArgumentException();
            var riderE = await Repository.FindAsync(riderId) ?? throw new EntityNotFoundException("Manager does not exist");

            riderE.Status = EntityStatusConsts.WarningOne;
            await Repository.UpdateAsync(riderE);
        }

        public async Task WarnSecondAsync(int riderId)
        {
            if (riderId < 1) throw new ArgumentException();
            var riderE = await Repository.FindAsync(riderId) ?? throw new EntityNotFoundException("Manager does not exist");

            riderE.Status = EntityStatusConsts.WarningTwo;
            await Repository.UpdateAsync(riderE);
        }

        public async Task UnblockAsync(int riderId)
        {
            if (riderId < 1) throw new ArgumentException();
            await WarnFirstAsync(riderId);
        }

        public async Task SelectOrder(int orderId)
        {
            var userId = CurrentUser.Id;

        }
    }
}
