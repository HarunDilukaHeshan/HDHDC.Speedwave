using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class DistrictAppService
        : CrudAppService<
            DistrictEntity,
            DistrictDto,
            string,
            PagedAndSortedResultRequestDto,
            DistrictCreateDto>,
        IDistrictAppService
    { 
        public DistrictAppService(IRepository<DistrictEntity, string> repository)
            : base(repository)
        { }

        public override async Task DeleteAsync(string id)
        {
            var entity = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();
            await Repository.HardDeleteAsync(entity);
        }
    }
}
