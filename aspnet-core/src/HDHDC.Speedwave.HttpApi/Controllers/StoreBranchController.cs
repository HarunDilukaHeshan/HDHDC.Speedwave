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
    public class StoreBranchController : SpeedwaveController
    {
        private IStoreBranchAppService StoreBranchAppService { get; }

        public StoreBranchController(IStoreBranchAppService storeBranchAppService)
        {
            StoreBranchAppService = storeBranchAppService;
        }

        [HttpGet]
        public async Task<StoreBranchDto[]> GetAll([FromQuery]int cityId)
        {            
            return await StoreBranchAppService.GetBranchesWithinRadius(cityId);
        }

        [HttpGet("{id}")]
        public async Task<StoreBranchDto> Get([FromRoute]int id)
        {
            return await StoreBranchAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<StoreBranchDto> Create([FromBody]StoreBranchCreateDto dto)
        {
            return await StoreBranchAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<StoreBranchDto> Update([FromRoute]int id, [FromBody]StoreBranchUpdateDto dto)
        {
            return await StoreBranchAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await StoreBranchAppService.DeleteAsync(id);
        }

        [HttpGet("{id}/openingdays")]
        public async Task<StoreOpenDayDto[]> GetAllOpenDayDto([FromRoute]int id)
        {
            return await StoreBranchAppService.GetAllOpenDayAsync(id);
        }

        [HttpGet("{id}/openingdays/{dofw}")]
        public async Task<StoreOpenDayDto> GetOpenDayDto([FromRoute]int id, [FromRoute]DayOfWeek dofw)
        {
            return await StoreBranchAppService.GetOpenDayAsync(id, dofw);
        }

        [HttpPost("{id}/openingdays")]
        public async Task<StoreOpenDayDto> CreateOpenDay([FromRoute]int id, [FromBody]StoreOpenDayDto dto)
        {
            return await StoreBranchAppService.CreateOpenDayAsync(id, dto);
        }

        [HttpPut("{id}/openingdays")]
        public async Task<StoreOpenDayDto[]> UpdateOpenDay(
            [FromRoute]int id, 
            [FromBody]StoreOpenDayDto[] dtoArr)
        {            
                return await StoreBranchAppService.UpdateOpenDayAsync(id, dtoArr);            
        }

        [HttpGet("{id}/closingdate")]
        public async Task<StoreClosingDateDto[]> GetAllClosingDates([FromRoute]int id)
        {
            return await StoreBranchAppService.GetAllClosingDateAsync(id);
        }

        [HttpPost("{id}/closingdate")]
        public async Task<StoreClosingDateDto> CreateClosingDate([FromRoute]int id, [FromBody]StoreClosingDateDto dto)
        {
            return await StoreBranchAppService.CreateClosingDateAsync(id, dto.ClosingDate);
        }

        [HttpDelete("{id}/closingdate/{closingdateid}")]
        public async Task DeleteClosingDate([FromRoute]int id, [FromRoute]int closingdateid)
        {
            await StoreBranchAppService.DeleteClosingDateAsync(closingdateid);
        }

        [HttpGet("{id}/items")]
        public async Task<ItemStoreBranchDto[]> GetAllItems(
            [FromRoute]int id, 
            [FromQuery]string keywords,
            [FromQuery]int skipCount = 0, 
            [FromQuery]int maxResultCount = 10, 
            [FromQuery]bool includeDetails = false)
        {
            if (string.IsNullOrWhiteSpace(keywords))
                return await StoreBranchAppService.GetAllItemsAsync(id, skipCount, maxResultCount, includeDetails);

            return await StoreBranchAppService.SearchAsync(keywords, id, skipCount, maxResultCount, includeDetails);
        }

        [HttpPost("{id}/items")]
        public async Task<ItemStoreBranchDto> AddItem([FromRoute]int id, [FromBody]ItemStoreBranchDto dto)
        {            
            return await StoreBranchAppService.AddItemAsync(id, dto);                      
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task RemoveItem([FromRoute]int id, [FromRoute]int itemId)
        {
            await StoreBranchAppService.RemoveItemAsync(id, itemId);
        }
    }
}
