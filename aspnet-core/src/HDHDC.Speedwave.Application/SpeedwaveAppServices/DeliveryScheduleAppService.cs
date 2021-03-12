using HDHDC.Speedwave.Dependencies;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
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
    public class DeliveryScheduleAppService
        : CrudAppService<
            DeliveryScheduleEntity, 
            DeliveryScheduleDto,
            int,
            PagedAndSortedResultRequestDto,
            DeliveryScheduleCreateUpdateDto>,
        IDeliveryScheduleAppService
    {
        protected IRepository<StoreOpenDayEntity> StoreOpenDayRepository { get; }
        protected IRepository<StoreClosingDateEntity, int> StoreClosingDateRepository { get; }
        protected IRepository<StoreBranchEntity> StoreBranchRepository { get; }
        protected CitySelector CitySelector { get; }
        protected IRepository<AddressEntity, int> AddressRepository { get; }
        protected IDeliveryScheduleRepository DeliveryScheduleRepository { get; }

        public DeliveryScheduleAppService(
            IDeliveryScheduleRepository deliveryScheduleRepository,
            IRepository<DeliveryScheduleEntity, int> repository, 
            IRepository<StoreOpenDayEntity> storeOpenDayRepository, 
            IRepository<AddressEntity, int> addressRepository,            
            IRepository<StoreClosingDateEntity, int> storeClosingDateRepository, 
            IRepository<StoreBranchEntity> storeBranchRepository)
            : base(deliveryScheduleRepository)
        {
            StoreOpenDayRepository = storeOpenDayRepository;
            AddressRepository = addressRepository;
            StoreClosingDateRepository = storeClosingDateRepository;
            StoreBranchRepository = storeBranchRepository;
            DeliveryScheduleRepository = deliveryScheduleRepository;
        }

        public override async Task<DeliveryScheduleDto> CreateAsync(DeliveryScheduleCreateUpdateDto input)
        {
            var entity = await Repository.FindAsync(e => e.IsDeleted == true && TimeSpan.Compare(e.TimePeriod, input.TimePeriod) == 0);

            if (entity != null) throw new BusinessException("A delivery schedule already exists with the specified time period");

            return await base.CreateAsync(input);
        }

        public override async Task<DeliveryScheduleDto> UpdateAsync(int id, DeliveryScheduleCreateUpdateDto input)
        {
            _= await Repository.FindAsync(id) ?? throw new EntityNotFoundException();

            var entity = await Repository.FindAsync(e => e.Id != id && TimeSpan.Compare(e.TimePeriod, input.TimePeriod) == 0);
            if (entity != null) throw new BusinessException("A delivery schedule already exists with the specified time period");

            await Repository.DeleteAsync(id);

            return await CreateAsync(input);
        }

        public async Task<IList<DeliveryScheduleDto>> GetCompatibleListAsync(int addressId, int[] itemsList)
        {
            var customerClaim = CurrentUser.FindClaim("customerId") ?? throw new BusinessException();
            var addressE = await AddressRepository
                .FindAsync(e => e.Id == addressId && e.CustomerID == int.Parse(customerClaim.Value)) 
                ?? throw new EntityNotFoundException();

            return null;
        }

        public async Task<DeliveryScheduleDto[]> GetAllCompatiblesAsync(int addressId, int[] itemIds)
        {
            //var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            var dseArr = await DeliveryScheduleRepository.GetCompatibleSchedules(new Guid(), addressId, itemIds);

            return ObjectMapper.Map<DeliveryScheduleEntity[], DeliveryScheduleDto[]>(dseArr);
        }
    }
}

