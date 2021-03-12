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
    public class ItemController : SpeedwaveController
    {
        private IItemAppService ItemAppService { get; }
        private IItemCategoryAppService ItemCategoryAppService { get; }

        public ItemController(IItemAppService itemAppService, IItemCategoryAppService itemCategoryAppService)
        {
            ItemAppService = itemAppService;
            ItemCategoryAppService = itemCategoryAppService;
        }

        [HttpGet("{id}")]
        public async Task<ItemDto> Get([FromRoute]int id)
        {
            return await ItemAppService.GetAsync(id);
        }

        // required params: select random : seed, maxResultCount 
        // required params: search : keywords, skipCount, maxResultCount
        [HttpGet]
        public async Task<ItemDto[]> GetList(
            string keywords = null, 
            int seed = 0, 
            int skipCount = 0, 
            int maxResultCount = 10, 
            int[] itemIdsArr = null)
        {
            if (seed < 0 || skipCount < 0 || maxResultCount < 0) throw new ArgumentException();

            if (itemIdsArr != null && itemIdsArr.Length > 0)
                return await ItemAppService.GetItemsWithDetailAsync(itemIdsArr);            
            else if (keywords == null)
                return await ItemAppService.SelectAsync(seed, maxResultCount);            
            else
                return await ItemAppService.SearchAsync(keywords, skipCount, maxResultCount);
        }

        [HttpPost]
        public async Task<ItemDto> Create([FromBody]ItemCreateDto dto)
        {

            return await ItemAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<ItemDto> Update([FromRoute]int id, [FromBody]ItemUpdateDto dto)
        {
            return await ItemAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await ItemAppService.DeleteAsync(id);
        }

        [HttpPost("category")]
        public async Task<ItemCategoryDto> CreateItemCategory([FromBody]ItemCategoryDto dto)
        {
            return await ItemCategoryAppService.CreateAsync(dto);
        }

        [HttpDelete("category")]
        public async Task DeleteItemCategory([FromBody]ItemCategoryKey key)
        {
            await ItemCategoryAppService.DeleteAsync(key);
        }

        [HttpGet("search")]
        public async Task<ItemDto[]> SearchAsync(
            [FromQuery]string keywords = "",
            [FromQuery]int skipCount = 0,
            [FromQuery]int maxResultCount = 10)
        {
            return await ItemAppService.SearchAsync(keywords);
        }

        [HttpGet("itempic/{itempic}")]
        public async Task<ThumbnailDto> GetThumbnail([FromRoute]string itempic)
        {
            return await ItemAppService.GetThumbnail(itempic);
        }        

        [HttpGet("{id}/itemcategory")]
        public async Task<ItemCategoryDto[]> GetAllItemCategory([FromRoute]int id)
        {
            return await ItemAppService.GetAllItemCategory(id);
        }

        [HttpPut("{id}/itemcategory")]
        public async Task UpdateItemCategory([FromRoute]int id, [FromBody]ItemCategoryDto[] dtosArr)
        {

            var idsList = new List<int>();

            foreach (var dto in dtosArr) idsList.Add(dto.CategoryID);

            await ItemAppService.UpdateItemCategory(id, idsList.ToArray());

        }

        [HttpGet("{id}/itempic")]
        public async Task<BlobFileDto[]> GetAllItemPic([FromRoute]int id)
        {
            return await ItemAppService.GetAllItemPic(id);
        }

        [HttpPost("{id}/itempic")]
        public async Task<BlobFileDto> AddItemPic([FromRoute]int id, [FromBody]BlobFileDto dto)
        {
            return await ItemAppService.AddItemPic(id, dto);
        }

        [HttpDelete("{id}/itempic/{filename}")]
        public async Task DeleteItemPic([FromRoute]int id, [FromRoute]string filename)
        {
            await ItemAppService.RemoveItemPic(filename);
        }        
    }
}
