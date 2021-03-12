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
    public class UnitController : SpeedwaveController
    {
        private IUnitAppService UnitAppService { get; }

        public UnitController(IUnitAppService unitAppService)
        {
            UnitAppService = unitAppService;
        }

        [HttpGet]
        public async Task<UnitDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await UnitAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<UnitDto> Get([FromRoute]string id)
        {
            return await UnitAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<UnitDto> Create([FromBody]UnitDto dto)
        {            
            return await UnitAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<UnitDto> Update([FromRoute]string id, [FromBody]UnitDto dto)
        {
            return await UnitAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]string id)
        {
            await UnitAppService.DeleteAsync(id);
        }
    }
}
