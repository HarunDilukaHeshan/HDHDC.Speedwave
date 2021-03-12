using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IAddressAppService : IApplicationService
    {
        Task<AddressDto> CreateAsync(AddressCreateDto addressCreateDto);
        Task<AddressDto> GetAsync(int id);
        Task<IList<AddressDto>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
