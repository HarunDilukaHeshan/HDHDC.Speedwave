using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class CategoryController : SpeedwaveController
    {
        private ICategoryAppService CategoryAppService { get; }
        private ICustomerAppService CustomerAppService { get; }

        public CategoryController(
            ISlideShowAppService slideShowAppService,
            ICategoryAppService categoryAppService, 
            ICustomerAppService customerAppService)
        {
            CategoryAppService = categoryAppService;
            CustomerAppService = customerAppService;
           // slideShowAppService.SaveAsync(null).GetAwaiter();
        }

        [HttpGet]
        public async Task<CategoryDto[]> GetAll()
        {           
            await CustomerAppService.GetAllAddressDtoAsync();
            var categories = await CategoryAppService.GetAllAsync();

            return categories.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<CategoryDto> Get([FromRoute]int id)
        {
            return await CategoryAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<CategoryDto> Create([FromBody]CategoryCreateDto dto)
        {
            return await CategoryAppService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public async Task<CategoryDto> Update(
            [FromRoute]int id,
            [FromBody]CategoryUpdateDto dto)
        {            
            return await CategoryAppService.UpdateAsync(id, dto);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)

        {
            await CategoryAppService.DeleteAsync(id);
        }

        [HttpGet("thumbnail/{thumbnail}")]
        public async Task<ThumbnailDto> GetThumbnail([FromRoute]string thumbnail)
        {
            var b64 = await CategoryAppService.GetThumbnail(thumbnail);

            return new ThumbnailDto { Base64DataUrl = b64 };
        }
    }
}
