using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface ICustomerAppService
        : ICrudAppService<
            CustomerDto,
            int,
            PagedAndSortedResultRequestDto,
            CustomerCreateDto, 
            CustomerUpdateDto>
    {
        Task<CustomerDto> GetCustomerByUserId(Guid userId);
        Task<AddressDto[]> GetAllAddressDtoAsync();
        Task<AddressDto> GetAddressDtoAsync(int id);
        Task<AddressDto> CreateAddressDto(AddressCreateDto dto);
        Task<AddressDto> UpdateAddressDto(int id, AddressUpdateDto dto);
        Task DeleteAddressAsync(int id);
    }
}
