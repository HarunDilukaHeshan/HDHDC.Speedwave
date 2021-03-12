using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IDeliveryScheduleAppService
        : ICrudAppService<
            DeliveryScheduleDto,
            int,
            PagedAndSortedResultRequestDto, 
            DeliveryScheduleCreateUpdateDto>
    {
        Task<IList<DeliveryScheduleDto>> GetCompatibleListAsync(int addressId, int[] itemsList);
        Task<DeliveryScheduleDto[]> GetAllCompatiblesAsync(int addressId, int[] itemIds);
    }
}
