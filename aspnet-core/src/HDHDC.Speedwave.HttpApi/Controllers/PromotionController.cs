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
    public class PromotionController : SpeedwaveController
    {
        private IPromotionAppService PromotionAppService { get; }

        public PromotionController(IPromotionAppService promotionAppService)
        {
            PromotionAppService = promotionAppService;
        }

        [HttpGet]
        public async Task<PromotionDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await PromotionAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<PromotionDto> Get([FromRoute]int id)
        {
            return await PromotionAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<PromotionDto> Create([FromBody]PromotionCreateDto dto)
        {
            return await PromotionAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<PromotionDto> Update([FromRoute]int id, [FromBody]PromotionUpdateDto dto)
        {
            return await PromotionAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await PromotionAppService.DeleteAsync(id);
        }
    }
}
