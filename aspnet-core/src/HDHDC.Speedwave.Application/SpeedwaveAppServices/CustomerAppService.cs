using System.Linq;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.Repositories.Services;
using Volo.Abp;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class CustomerAppService
        : CrudAppService<
            CustomerEntity,
            CustomerDto,
            int,
            PagedAndSortedResultRequestDto,
            CustomerCreateDto,
            CustomerUpdateDto>,
        ICustomerAppService
    {
        protected IAddressRepository AddressRepository { get; }

        public CustomerAppService(
            IAddressRepository addressRepository,
            IRepository<CustomerEntity, int> customerRepository)
            : base(customerRepository)
        {
            AddressRepository = addressRepository;
        }

        public virtual async Task<CustomerDto> GetCustomerByUserId(Guid userId)
        {            
            var customerE = await Repository.FindAsync(e => e.UserID == userId);
            var customerDto = new CustomerDto();

            if (customerE == null) return null;

            ObjectMapper.Map(customerE, customerDto);

            return customerDto;
        }

        public virtual async Task<AddressDto[]> GetAllAddressDtoAsync()
        {
            var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            var addrEArr = await AddressRepository.GetAllAsync((Guid)userId);            
            
            return ObjectMapper.Map<AddressEntity[], AddressDto[]>(addrEArr);
        }

        public virtual async Task<AddressDto> GetAddressDtoAsync(int id)
        {
            var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            var addrE = await AddressRepository.GetAsync((Guid)userId, id);
            return ObjectMapper.Map<AddressEntity, AddressDto>(addrE);
        }

        public virtual async Task<AddressDto> CreateAddressDto(AddressCreateDto dto)
        {
            var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            var entity = ObjectMapper.Map<AddressCreateDto, AddressEntity>(dto);
            entity = await AddressRepository.InsertAsync((Guid)userId, entity);

            return ObjectMapper.Map<AddressEntity, AddressDto>(entity);
        }

        public virtual async Task<AddressDto> UpdateAddressDto(int id, AddressUpdateDto dto)
        {
            var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            var entity = ObjectMapper.Map<AddressUpdateDto, AddressEntity>(dto);
            entity = await AddressRepository.UpdateAsync((Guid)userId, id, entity);
            return ObjectMapper.Map<AddressEntity, AddressDto>(entity);
        }

        public virtual async Task DeleteAddressAsync(int id)
        {
            var userId = (CurrentUser.IsAuthenticated) ? CurrentUser.Id : throw new BusinessException();
            await AddressRepository.DeleteAsync((Guid)userId, id);
        }
    }
}
