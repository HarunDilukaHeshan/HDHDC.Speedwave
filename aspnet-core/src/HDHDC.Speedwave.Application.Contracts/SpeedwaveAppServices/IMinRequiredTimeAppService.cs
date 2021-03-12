using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IMinRequiredTimeAppService
        : ICrudAppService<
            MinRequiredTimeDto,
            int,
            PagedAndSortedResultRequestDto,
            MinRequiredTimeDto>
    { }
}
