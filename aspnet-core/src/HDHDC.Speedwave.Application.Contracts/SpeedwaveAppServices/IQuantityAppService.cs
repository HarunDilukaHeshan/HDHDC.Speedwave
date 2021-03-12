using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IQuantityAppService
        : ICrudAppService<
            QuantityDto,
            int,
            PagedAndSortedResultRequestDto,
            QuantityDto>
    { }
}
