using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.Extensions.Configuration;
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

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class StoreBranchAppService
        : CrudAppService<
            StoreBranchEntity,
            StoreBranchDto,
            int,
            PagedAndSortedResultRequestDto,
            StoreBranchCreateDto,
            StoreBranchUpdateDto>,
        IStoreBranchAppService
    {
        protected IStoreBranchRepository StoreBranchRepository { get; }
        protected IRepository<StoreOpenDayEntity> StoreOpenDayRepository { get; }
        protected IConfiguration Configuration { get; }
        protected IRepository<CityEntity, int> CityRepository { get; }
        protected IRepository<StoreChainEntity, int> StoreChainRepository { get; }
        protected IRepository<StoreClosingDateEntity, int> StoreClosingDateRepository { get; }
        protected IItemStoreBranchRepository ItemStoreBranchRepository { get; }
        protected IRepository<ItemEntity, int> ItemRepository { get; }
        public StoreBranchAppService(
            IRepository<ItemEntity, int> itemRepository,
            IItemStoreBranchRepository itemStoreBranchRepository,
            IStoreBranchRepository repository,
            IRepository<StoreOpenDayEntity> storeOpenDayRepository,
            IRepository<CityEntity, int> cityRepository,
            IRepository<StoreChainEntity, int> storeChainRepository,
            IRepository<StoreClosingDateEntity, int> storeClosingDateRepository,
            IConfiguration configuration)
            : base(repository)
        {
            Configuration = configuration;
            StoreOpenDayRepository = storeOpenDayRepository;
            CityRepository = cityRepository;
            StoreChainRepository = storeChainRepository;
            StoreClosingDateRepository = storeClosingDateRepository;
            ItemStoreBranchRepository = itemStoreBranchRepository;
            ItemRepository = itemRepository;
            StoreBranchRepository = repository;
        }

        public override async Task<StoreBranchDto> GetAsync(int id)
        {
            var entity = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();
            await Repository.EnsurePropertyLoadedAsync(entity, e => e.CityEntity);
            return ObjectMapper.Map<StoreBranchEntity, StoreBranchDto>(entity);
        }

        public override async Task<StoreBranchDto> CreateAsync(StoreBranchCreateDto input)
        {
            _ = StoreChainRepository.GetAsync(input.StoreChainID) ?? throw new BusinessException("Invalid storeChain");

            _ = CityRepository.GetAsync(input.CityID) ?? throw new BusinessException("Invalid city");

            var storeBranchE = ObjectMapper.Map<StoreBranchCreateDto, StoreBranchEntity>(input);

            await Repository.InsertAsync(storeBranchE, autoSave: true);

            foreach (var dayoftheweek in Enum.GetNames(typeof(DayOfWeek)))
            {
                var storeOpenDayE = new StoreOpenDayEntity(storeBranchE.Id, Enum.Parse<DayOfWeek>(dayoftheweek))
                {
                    OpeningTime = new TimeSpan(7, 30, 0),
                    ClosingTime = new TimeSpan(22, 30, 0)
                };
                await StoreOpenDayRepository.InsertAsync(storeOpenDayE, autoSave: true);
            }

            return ObjectMapper.Map<StoreBranchEntity, StoreBranchDto>(storeBranchE);
        }

        public async Task<StoreClosingDateDto[]> GetAllClosingDateAsync(int branchId)
        {
            var eList = await AsyncExecuter.ToListAsync(StoreClosingDateRepository.Where(e => e.StoreBranchID == branchId));
            return ObjectMapper.Map<StoreClosingDateEntity[], StoreClosingDateDto[]>(eList.ToArray())
                .OrderBy(dto => dto.ClosingDate)
                .ToArray();
        }

        public async Task<StoreClosingDateDto> CreateClosingDateAsync(int branchId, DateTime closingDate)
        {
            var storeBranchE = await Repository.FindAsync(branchId) ?? throw new EntityNotFoundException();
            var storeClosingDateE = await StoreClosingDateRepository
                .FindAsync(e =>
                e.StoreBranchID == branchId &&
                e.ClosingDate.Year == closingDate.Year &&
                e.ClosingDate.Month == closingDate.Month &&
                e.ClosingDate.Day == closingDate.Day);

            if (storeClosingDateE != null) throw new BusinessException();

            var yesterday = DateTime.Now.Subtract(TimeSpan.FromDays(1)).ClearTime();
            var cDate = closingDate.ClearTime();

            if (DateTime.Compare(cDate, yesterday) <= 0) throw new BusinessException();

            storeClosingDateE = await StoreClosingDateRepository.InsertAsync(new StoreClosingDateEntity(branchId, closingDate), true);

            return ObjectMapper.Map<StoreClosingDateEntity, StoreClosingDateDto>(storeClosingDateE);
        }

        public async Task DeleteClosingDateAsync(int Id)
        {
            await StoreClosingDateRepository.DeleteAsync(Id);
        }

        public async Task<StoreOpenDayDto[]> GetAllOpenDayAsync(int branchId)
        {
            var itemsQ = await AsyncExecuter.ToListAsync(StoreOpenDayRepository.Where(e => e.StoreBranchID == branchId));
            return ObjectMapper.Map<StoreOpenDayEntity[], StoreOpenDayDto[]>(itemsQ.ToArray());
        }

        public async Task<StoreOpenDayDto> GetOpenDayAsync(int branchId, DayOfWeek dayOfWeek)
        {
            var entity = await StoreOpenDayRepository.GetAsync(e => e.StoreBranchID == branchId && e.DayOfWeek == dayOfWeek)
                ?? throw new EntityNotFoundException();
            return ObjectMapper.Map<StoreOpenDayEntity, StoreOpenDayDto>(entity);
        }

        public async Task<StoreOpenDayDto> CreateOpenDayAsync(int branchId, StoreOpenDayDto dto)
        {
            var branchE = await Repository.GetAsync(branchId) ?? throw new EntityNotFoundException();

            var openDayE = await StoreOpenDayRepository.GetAsync(e => e.StoreBranchID == branchId && e.DayOfWeek == dto.DayOfWeek);

            dto.StoreBranchID = branchId;

            var entity = ObjectMapper.Map<StoreOpenDayDto, StoreOpenDayEntity>(dto);

            await StoreOpenDayRepository.InsertAsync(entity, autoSave: true);

            return ObjectMapper.Map<StoreOpenDayEntity, StoreOpenDayDto>(entity);
        }

        public async Task<StoreOpenDayDto[]> UpdateOpenDayAsync(int branchId, StoreOpenDayDto[] dtoArr)
        {
            var storeBranchE = await Repository.GetAsync(branchId) ?? throw new EntityNotFoundException();
            var openDayEs = await AsyncExecuter.ToListAsync(StoreOpenDayRepository.Where(e => e.StoreBranchID == branchId));

            foreach (var entity in openDayEs)
            {
                var dto = dtoArr.SingleOrDefault(e => e.DayOfWeek == entity.DayOfWeek);
                if (dto == null)
                {
                    await StoreOpenDayRepository.DeleteAsync(entity, autoSave: true);
                }
                else
                {
                    entity.OpeningTime = dto.OpeningTime;
                    entity.ClosingTime = dto.ClosingTime;
                    await StoreOpenDayRepository.UpdateAsync(entity, autoSave: true);
                }
            }

            foreach (var dto in dtoArr)
            {
                var entity = openDayEs.SingleOrDefault(e => e.DayOfWeek == dto.DayOfWeek);
                if (entity == null)
                {
                    var cEntity = ObjectMapper.Map<StoreOpenDayDto, StoreOpenDayEntity>(dto);
                    await StoreOpenDayRepository.InsertAsync(cEntity, autoSave: true);
                }
            }
            openDayEs = await AsyncExecuter.ToListAsync(StoreOpenDayRepository.Where(e => e.StoreBranchID == branchId));
            return ObjectMapper.Map<StoreOpenDayEntity[], StoreOpenDayDto[]>(openDayEs.ToArray());
        }

        public async Task DeleteOpenDayAsync(int branchId, DayOfWeek dayOfWeek)
        {
            await StoreOpenDayRepository.DeleteAsync(e => e.StoreBranchID == branchId && e.DayOfWeek == dayOfWeek);
        }

        public async Task<ItemStoreBranchDto[]> GetAllItemsAsync(
            int branchId,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false)
        {
            var entities = await ItemStoreBranchRepository
                .GetListAsync(e => e.StoreBranchID == branchId, skipCount, maxResultCount, includeDetails);

            return ObjectMapper.Map<ItemStoreBranchEntity[], ItemStoreBranchDto[]>(entities);
        }

        public async Task<ItemStoreBranchDto[]> SearchAsync(
            string keywords,
            int branchId,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false)
        {
            var entities = await ItemStoreBranchRepository
                .SearchAsync(keywords, branchId, skipCount, maxResultCount, includeDetails);

            return ObjectMapper.Map<ItemStoreBranchEntity[], ItemStoreBranchDto[]>(entities);
        }

        public async Task<ItemStoreBranchDto> AddItemAsync(int branchId, ItemStoreBranchDto dto)
        {
            if (dto == null) throw new ArgumentNullException();

            _= await Repository.GetAsync(branchId) ?? throw new BusinessException("Invalid branch id");
            _= await ItemRepository.GetAsync(dto.ItemID) ?? throw new BusinessException("Invalid item id");

            var entity = await ItemStoreBranchRepository.InsertAsync(new ItemStoreBranchEntity(dto.ItemID, branchId));
            return ObjectMapper.Map<ItemStoreBranchEntity, ItemStoreBranchDto>(entity);
        }

        public async Task RemoveItemAsync(int branchId, int itemId)
        {
            var entity = await ItemStoreBranchRepository.GetAsync(e => e.StoreBranchID == branchId && e.ItemID == itemId)
                ?? throw new EntityNotFoundException();

            await ItemStoreBranchRepository.DeleteAsync(entity);
        }
        
        public async Task<StoreBranchDto[]> GetBranchesWithinRadius(int cityId)
        {
            var sbEs = await StoreBranchRepository.GetAllBranchAroundTheCityAsync(cityId);
            return ObjectMapper.Map<StoreBranchEntity[], StoreBranchDto[]>(sbEs);
        }
    }
}
