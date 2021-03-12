using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class ProvinceController : SpeedwaveController
    {
        private IProvinceAppService ProvinceAppService { get; }

        public ProvinceController(IProvinceAppService provinceAppService)
        {
            ProvinceAppService = provinceAppService;
        }

        [HttpGet]
        public async Task<ProvinceDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return await ProvinceAppService.GetListAsync(input);
        }

        [HttpGet("{id}")]
        public async Task<ProvinceDto> Get([FromRoute]string id)
        {
            return await ProvinceAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ProvinceDto> Create([FromBody]ProvinceDto dto)
        {
            return await ProvinceAppService.CreateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]string id)
        {
            await ProvinceAppService.DeleteAsync(id);
        }
    }
}
