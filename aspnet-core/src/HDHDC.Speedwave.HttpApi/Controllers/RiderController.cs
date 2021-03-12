using HDHDC.Speedwave.AccountAppServices;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using HDHDC.Speedwave.SpeedyConsts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class RiderController : SpeedwaveController
    {
        private IRiderAppService RiderAppService { get; }

        private IRiderAccountAppService RiderAccountAppService { get; }

        public RiderController(
            IRiderAppService riderAppService, 
            IRiderAccountAppService riderAccountAppService)
        {
            RiderAppService = riderAppService;
            RiderAccountAppService = riderAccountAppService;
        }

        [HttpGet]
        public async Task<RiderDto[]> GetAll(
            [FromQuery]int cityId = -1, 
            [FromQuery]int skipCount = 0, 
            [FromQuery]int maxResultCount = 10)
        {
            var input = new PagedAndSortedResultRequestDto { SkipCount = skipCount, MaxResultCount = maxResultCount };
            if (cityId > -1)
                return await RiderAppService.GetListAsync(cityId, input, true);

            return await RiderAppService.GetListAsync(input, true);
        }

        [HttpGet("{id}")]
        public async Task<RiderDto> Get([FromRoute]int id)
        {
            return await RiderAppService.GetAsync(id);
        }

        [HttpGet("current")]
        public async Task<RiderDto> GetCurrentRider()
        {
            return await RiderAppService.GetCurrentRiderAsync();
        }

        [HttpGet("current/coverage")]
        public async Task<CityDto[]> GetRiderCoverage()
        {
            return await RiderAppService.GetRiderCoverageAsync();
        }

        [HttpPut("current/coverage")]
        public async Task<CityDto[]> UpdateRiderCoverage([FromQuery]int[] cityIds)
        {
            return await RiderAppService.UpdateRiderCoverageAsync(cityIds);
        }

        [HttpPost]
        public async Task<AppUserDto> Create([FromBody]RiderCreateDto dto)
        {
            return await RiderAccountAppService.RegisterAsync(dto.UserCreateDto, dto);
        }

        [HttpPut("{id}")]
        public async Task<RiderDto> Update([FromRoute]int id, [FromBody]RiderUpdateDto dto)
        {
            return await RiderAppService.UpdateAsync(id, dto);
        }

        [HttpPut("{id}/status")]
        public async Task<RiderDto> UpdateStatus([FromRoute]int id, [FromQuery]string statusId)
        {
            switch (statusId)
            {
                case EntityStatusConsts.WarningOne:
                    await RiderAppService.WarnFirstAsync(id);
                    break;
                case EntityStatusConsts.WarningTwo:
                    await RiderAppService.WarnSecondAsync(id);
                    break;
                case EntityStatusConsts.Blocked:
                    await RiderAppService.BlockAsync(id);
                    break;
                case "unblocked":
                    await RiderAppService.UnblockAsync(id);
                    break;
                default:
                    throw new BusinessException("Invalid manager status");
            }

            return await RiderAppService.GetAsync(id);
        }

        [HttpGet("search")]
        public async Task<RiderDto[]> Search(
            [FromQuery]string words, 
            [FromQuery]int skipCount = 0, 
            [FromQuery]int maxResultCount = 10)
        {
            words ??= "";
            return await RiderAppService.SearchAsync(words, skipCount, maxResultCount);
        }
    }
}
