using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class SlideShowController : SpeedwaveController
    {
        protected ISlideShowAppService SlideShowAppService { get; }

        public SlideShowController(ISlideShowAppService slideShowAppService)
        {
            SlideShowAppService = slideShowAppService;
        }

        [HttpGet("{fileName}")]
        public async Task<SlideShowPictureDto> Get([FromRoute]string fileName)
        {
            return await SlideShowAppService.GetAsync(fileName);
        }

        [HttpGet]
        public async Task<SlideShowPictureDto[]> GetAll()
        {
            return await SlideShowAppService.GetArrayAsync();
        }

        [HttpPost]
        public async Task<SlideShowPictureDto> Create([FromBody]SlideShowPictureDto dto)
        {
            return await SlideShowAppService.SaveAsync(dto);
        }

        [HttpPut("{fileName}")]
        public async Task<SlideShowPictureDto> Update([FromRoute] string fileName, [FromBody]SlideShowPictureDto dto)
        {
            return await SlideShowAppService.UpdateAsync(fileName, dto);
        }

        [HttpDelete("{fileName}")]
        public async Task Delete([FromRoute]string fileName)
        {
            await SlideShowAppService.DeleteAsync(fileName);
        }
    }
}
