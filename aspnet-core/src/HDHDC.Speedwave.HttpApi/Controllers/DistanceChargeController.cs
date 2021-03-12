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
    public class DistanceChargeController : SpeedwaveController
    {
        private IDistanceChargeAppService DistanceChargeAppService { get; }

        public DistanceChargeController(IDistanceChargeAppService distanceChargeAppService)
        {
            DistanceChargeAppService = distanceChargeAppService;
        }

        [HttpGet]
        public async Task<DistanceChargeDto[]> GetAll()
        {                       
            var input = new PagedAndSortedResultRequestDto();
            return (await DistanceChargeAppService.GetListAsync(input)).Items.ToArray();           
        }

        [HttpGet("{id}")]
        public async Task<DistanceChargeDto> Get([FromRoute]int id)
        {
            return await DistanceChargeAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<DistanceChargeDto> Create([FromBody]DistanceChargeCreateUpdateDto dto)
        {
            return await DistanceChargeAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<DistanceChargeDto> Update([FromRoute]int id, [FromBody]DistanceChargeCreateUpdateDto dto)
        {
            return await DistanceChargeAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await DistanceChargeAppService.DeleteAsync(id);
        }
    }
}
