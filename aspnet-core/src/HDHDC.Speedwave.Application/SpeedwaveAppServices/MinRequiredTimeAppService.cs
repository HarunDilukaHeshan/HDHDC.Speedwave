using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class MinRequiredTimeAppService
        : CrudAppService<
            MinRequiredTimeEntity,
            MinRequiredTimeDto,
            int,
            PagedAndSortedResultRequestDto,
            MinRequiredTimeDto>,
        IMinRequiredTimeAppService
    {
        public MinRequiredTimeAppService(IRepository<MinRequiredTimeEntity, int> repository)
            : base(repository)
        { }
    }
}
