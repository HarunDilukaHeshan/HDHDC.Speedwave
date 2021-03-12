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
    public class SubtotalPercentageAppService
        : CrudAppService<
            SubtotalPercentageEntity, 
            SubtotalPercentageDto, 
            int, 
            PagedAndSortedResultRequestDto,
            SubtotalPercentageCreateUpdateDto>,
        ISubtotalPercentageAppService
    {
        public SubtotalPercentageAppService(IRepository<SubtotalPercentageEntity, int> repository)
            : base(repository)
        { }

        public override async Task<SubtotalPercentageDto> CreateAsync(SubtotalPercentageCreateUpdateDto input)
        {
            var newEntity = ObjectMapper.Map<SubtotalPercentageCreateUpdateDto, SubtotalPercentageEntity>(input);

            var entity = await Repository.FindAsync(e => e.From == input.From);

            if (entity != null) throw new BusinessException("The column 'From' cannot have duplicate values");

            if (await Repository.GetCountAsync() == 0 && newEntity.From != 0)
                throw new BusinessException("First subtotal percentage instance must hold the value 0 for 'From'");

            return await base.CreateAsync(input);
        }        

        public override async Task<SubtotalPercentageDto> UpdateAsync(int id, SubtotalPercentageCreateUpdateDto input)
        {
            var subtotalPercentageE = await Repository.FindAsync(id) ?? throw new EntityNotFoundException();

            var entity = await Repository.FindAsync(e => e.IsDeleted == false && e.Id != id && e.From == input.From);
            if (entity != null) throw new BusinessException();

            await base.DeleteAsync(id);

            subtotalPercentageE = await Repository.InsertAsync(new SubtotalPercentageEntity(input.Percentage, input.From), true);

            return ObjectMapper.Map<SubtotalPercentageEntity, SubtotalPercentageDto>(subtotalPercentageE);
        }
    }
}
