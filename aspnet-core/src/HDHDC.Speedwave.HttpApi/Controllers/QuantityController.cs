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
    public class QuantityController : SpeedwaveController
    {
        private IQuantityAppService QuantityAppService { get; }

        public QuantityController(IQuantityAppService quantityAppService)
        {
            QuantityAppService = quantityAppService;
        }

        [HttpGet]
        public async Task<QuantityDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();           
            return (await QuantityAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<QuantityDto> Get([FromRoute]int id)
        {
            return await QuantityAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<QuantityDto> Create([FromBody]QuantityDto dto)
        {

            QuantityDto qdto = null;
            try
            {
                qdto = await QuantityAppService.CreateAsync(dto);
            }
            catch (Exception ex)
            {

            }

            return qdto;            
        }

        [HttpPut("{id}")]
        public async Task<QuantityDto> Update([FromRoute]int id, [FromBody]QuantityDto dto)
        {            
            return await QuantityAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await QuantityAppService.DeleteAsync(id);
        }
    }
}
