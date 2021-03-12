using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class QuantityAppService
        : CrudAppService<
            QuantityEntity,
            QuantityDto,
            int,
            PagedAndSortedResultRequestDto,
            QuantityDto>,
        IQuantityAppService
    {
        public QuantityAppService(IQuantityRepository repository)
            : base(repository)
        { }        
    }
}
