using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class DistanceChargeAppService 
        : CrudAppService<
            DistanceChargeEntity, 
            DistanceChargeDto, 
            int, 
            PagedAndSortedResultRequestDto,
            DistanceChargeCreateUpdateDto, 
            DistanceChargeCreateUpdateDto>,
        IDistanceChargeAppService
    {
        public DistanceChargeAppService(IRepository<DistanceChargeEntity, int> repository)
            : base(repository)
        { }        

        public override async Task<DistanceChargeDto> CreateAsync(DistanceChargeCreateUpdateDto input)
        {
            var newEntity = ObjectMapper.Map<DistanceChargeCreateUpdateDto, DistanceChargeEntity>(input);

            var entity = await Repository.FindAsync(e => e.From == input.From);

            if (entity != null) throw new BusinessException("The column 'From' cannot have duplicate values");

            if (await Repository.GetCountAsync() == 0 && newEntity.From != 0)
                throw new BusinessException("First distance charge instance must hold the value 0 for 'From'");

            return await base.CreateAsync(input);
        }

        public override async Task<DistanceChargeDto> UpdateAsync(int id, DistanceChargeCreateUpdateDto input)
        {
            var distanceChargeE = await Repository.FindAsync(id) ?? throw new EntityNotFoundException();

            var entity = await Repository.FindAsync(e => e.Id != id && e.From == input.From);
            if (entity != null) throw new BusinessException("The specified value for the 'From' column already exists in another instance");

            if (distanceChargeE.From == 0 && input.From != 0)
                throw new BusinessException("'From' value zero cannot be modified");

            await base.DeleteAsync(id);

            distanceChargeE = await Repository.InsertAsync(new DistanceChargeEntity(input.Charge, input.From), true);

            return ObjectMapper.Map<DistanceChargeEntity, DistanceChargeDto>(distanceChargeE);
        }
    }
}
