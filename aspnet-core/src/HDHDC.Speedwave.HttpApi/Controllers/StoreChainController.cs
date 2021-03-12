using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class StoreChainController : SpeedwaveController
    {
        private IStoreChainAppService StoreChainAppService { get; }

        public StoreChainController(IStoreChainAppService storeChainAppService)
            : base()
        {
            StoreChainAppService = storeChainAppService;
        }

        [HttpGet]
        public async Task<StoreChainDto[]> GetAll()
        {
            var input = new PagedAndSortedResultRequestDto();
            return (await StoreChainAppService.GetListAsync(input)).Items.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<StoreChainDto> Get([FromRoute]int id)
        {
            return await StoreChainAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<StoreChainDto> Create([FromBody]StoreChainCreateDto dto)
        {
            StoreChainDto storeChainDto = null;

            try
            {
                storeChainDto = await StoreChainAppService.CreateAsync(dto);
            }
            catch(Exception ex)
            {

            }

            return storeChainDto;
        }

        [HttpPut("{id}")]
        public async Task<StoreChainDto> Update([FromRoute]int id, [FromBody]StoreChainUpdateDto dto)
        {
            return await StoreChainAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await StoreChainAppService.DeleteAsync(id);
        }

        [HttpGet("{id}/logo")]
        public async Task<ThumbnailDto> GetLogo([FromRoute]int id)
        {
            return await StoreChainAppService.GetLogoAsync(id);
        }

        [HttpPut("{id}/logo")]
        public async Task<StoreChainDto> UpdateLogo([FromRoute]int id, [FromBody]ThumbnailDto dto)
        {
            return await StoreChainAppService.UpdateLogoAsync(id, dto);
        }
    }
}
