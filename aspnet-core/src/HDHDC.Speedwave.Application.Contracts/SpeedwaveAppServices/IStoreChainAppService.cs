using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IStoreChainAppService
        : ICrudAppService<
            StoreChainDto,
            int,
            PagedAndSortedResultRequestDto,
            StoreChainCreateDto,
            StoreChainUpdateDto>
    {
        Task<StoreChainDto> UpdateLogoAsync(int id, ThumbnailDto dto);
        Task<ThumbnailDto> GetLogoAsync(int id);
    }
}
