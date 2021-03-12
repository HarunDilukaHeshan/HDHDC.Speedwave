using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class DeliveryScheduleController : SpeedwaveController
    {
        private IDeliveryScheduleAppService DeliveryScheduleAppService { get; }

        public DeliveryScheduleController(
            IDeliveryScheduleAppService deliveryScheduleAppService)
        {

            DeliveryScheduleAppService = deliveryScheduleAppService;
        }

        [HttpGet]
        public async Task<DeliveryScheduleDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await DeliveryScheduleAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<DeliveryScheduleDto> Get([FromRoute]int id)
        {
            return await DeliveryScheduleAppService.GetAsync(id);
        }

        [HttpGet("compatibles")]
        public async Task<DeliveryScheduleDto[]> GetCompatibles([FromQuery]int addressId, [FromQuery]int[] itemIds)
        {
           return await DeliveryScheduleAppService.GetAllCompatiblesAsync(addressId, itemIds);
        }

        [HttpPost]
        public async Task<DeliveryScheduleDto> Create([FromBody]DeliveryScheduleCreateUpdateDto dto)
        {
            var dd = dto.TimePeriod.Ticks;
            return await DeliveryScheduleAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<DeliveryScheduleDto> Update([FromRoute]int id, [FromBody]DeliveryScheduleCreateUpdateDto dto)
        {                                
            return await DeliveryScheduleAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await DeliveryScheduleAppService.DeleteAsync(id);
        }
    }
}
