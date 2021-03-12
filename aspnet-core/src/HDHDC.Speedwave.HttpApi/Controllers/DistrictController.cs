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
    public class DistrictController : SpeedwaveController
    {
        private IDistrictAppService DistrictAppService { get; }

        public DistrictController(IDistrictAppService districtAppService)
        {
            DistrictAppService = districtAppService;
        }

        [HttpGet]
        public async Task<DistrictDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await DistrictAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<DistrictDto> Get([FromRoute]string id)
        {
            return await DistrictAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<DistrictDto> Create([FromBody]DistrictCreateDto dto)
        {
            return await DistrictAppService.CreateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]string id)
        {
            await DistrictAppService.DeleteAsync(id);
        }
    }
}
