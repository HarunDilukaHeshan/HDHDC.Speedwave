using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class SubtotalPercentageController : SpeedwaveController
    {
        private ISubtotalPercentageAppService SubtotalPercentageAppService { get; }

        public SubtotalPercentageController(ISubtotalPercentageAppService subtotalPercentageAppService)
        {
            SubtotalPercentageAppService = subtotalPercentageAppService;
        }

        [HttpGet]
        public async Task<SubtotalPercentageDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await SubtotalPercentageAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<SubtotalPercentageDto> Get([FromRoute]int id)
        {
            return await SubtotalPercentageAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<SubtotalPercentageDto> Create([FromBody]SubtotalPercentageCreateUpdateDto dto)
        {
            return await SubtotalPercentageAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<SubtotalPercentageDto> Update([FromRoute]int id, [FromBody]SubtotalPercentageCreateUpdateDto dto)
        {
            return await SubtotalPercentageAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await SubtotalPercentageAppService.DeleteAsync(id);
        }
    }
}