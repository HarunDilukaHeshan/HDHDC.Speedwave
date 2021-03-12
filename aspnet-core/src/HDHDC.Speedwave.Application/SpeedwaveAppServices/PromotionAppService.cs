using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class PromotionAppService
        : CrudAppService<
            PromotionEntity,
            PromotionDto,
            int, 
            PagedAndSortedResultRequestDto,
            PromotionCreateDto, 
            PromotionUpdateDto>,
        IPromotionAppService
    {
        public PromotionAppService(IRepository<PromotionEntity, int> repository)
            : base(repository)
        { }

        public override async Task<PromotionDto> UpdateAsync(int id, PromotionUpdateDto input)
        {
            var entity = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            if (entity.IsOneTime && input.NoOfTimes != 1)
                throw new BusinessException("One time promotion must contain 1 as the value of NoOfTimes");
            else if (!entity.IsOneTime && input.NoOfTimes < 2)
                throw new BusinessException("NoOfTimes value must be greater than 1 for a multi-time promotion");

            return await base.UpdateAsync(id, input);
        }
    }
}
