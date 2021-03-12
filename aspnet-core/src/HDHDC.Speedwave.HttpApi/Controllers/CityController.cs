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
    public class CityController : SpeedwaveController
    {
        private ICityAppService CityAppService { get; }

        public CityController(ICityAppService cityAppService)
        {
            CityAppService = cityAppService;
        }

        [HttpGet]
        public async Task<CityDto[]> Search(
            [FromQuery]int skipCount = 0, 
            [FromQuery]int maxResultCount = 10, 
            [FromQuery]int cityId = 0,
            [FromQuery]string keyword = "", 
            [FromQuery]string districtId = null)
        {
            if (string.IsNullOrWhiteSpace(districtId))
                return await CityAppService.SearchAsync(skipCount, maxResultCount, keyword);

            if (cityId > 0)
                return await CityAppService.GetCityListWithinTheRadiusAsync(cityId);

            return await CityAppService.GetListAsync(districtId);
        }

        [HttpGet("{id}")]
        public async Task<CityDto> Get([FromRoute]int id)
        {
            return await CityAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<CityDto> Create([FromBody]CityCreateDto dto)
        {
            CityDto cDto = null;

            try
            {
                cDto = await CityAppService.CreateAsync(dto);
            }
            catch(Exception ex)
            {

            }


            return cDto;
        }

        [HttpPut("{id}")]
        public async Task<CityDto> Update([FromRoute]int id, [FromBody]CityUpdateDto dto)
        {
            return await CityAppService.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await CityAppService.DeleteAsync(id);
        }
    }
}
