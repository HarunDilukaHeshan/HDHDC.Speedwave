using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class MinRequiredTimeController : SpeedwaveController
    {
        private IMinRequiredTimeAppService MinRequiredTimeAppService { get; }

        public MinRequiredTimeController(IMinRequiredTimeAppService appService)
        {            
            MinRequiredTimeAppService = appService;            
        }

        [HttpGet]
        public async Task<MinRequiredTimeDto[]> GetList()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await MinRequiredTimeAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<MinRequiredTimeDto> Get([FromRoute]int id)
        {
            return await MinRequiredTimeAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<MinRequiredTimeDto> Create([FromBody]MinRequiredTimeDto dto)
        {
            return await MinRequiredTimeAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<MinRequiredTimeDto> Update([FromRoute]int id, [FromBody]MinRequiredTimeDto dto)
        {
            return await MinRequiredTimeAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await MinRequiredTimeAppService.DeleteAsync(id);
        }
    }
}
