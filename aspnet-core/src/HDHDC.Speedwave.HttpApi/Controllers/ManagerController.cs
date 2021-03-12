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
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class ManagerController : SpeedwaveController
    {
        private IManagerAccountAppService ManagerAccountAppService { get; }

        private IManagerAppService ManagerAppService { get; }

        public ManagerController(IManagerAccountAppService managerAccountAppService, IManagerAppService managerAppService)
        {
            ManagerAccountAppService = managerAccountAppService;
            ManagerAppService = managerAppService;
        }

        [HttpGet]
        public async Task<ManagerDto[]> GetAll([FromQuery]string districtId = "")
        {
            if (string.IsNullOrWhiteSpace(districtId))
                return await ManagerAppService.GetAllAsync(true);

            return await ManagerAppService.GetAllManagerByDistrictIdAsync(districtId, true);            
        }

        [HttpGet("/user/{userId}")]
        public async Task<ManagerDto> Get([FromRoute]Guid userId)
        {
            return await ManagerAppService.GetManagerByUserIdAsync(userId);
        }

        [HttpGet("{id}")]
        public async Task<ManagerDto> Get([FromRoute]int id)
        {
            return await ManagerAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<AppUserDto> Create([FromBody]ManagerCreateDto dto)
        {
            return await ManagerAccountAppService.RegisterAsync(dto.UserCreateDto, dto.DistrictID);
        }

        [HttpPut("{id}")]
        public async Task<ManagerDto> Update([FromRoute]int id, [FromBody]ManagerUpdateDto dto)
        {
            await ManagerAppService.ChangeDistrictAsync(id, new DistrictDto { Id = dto.DistrictID });

            return await ManagerAppService.GetAsync(id);
        }

        [HttpPut("{id}/status")]
        public async Task<ManagerDto> UpdateStatus([FromRoute]int id, [FromQuery]string statusId)
        {
            switch (statusId)
            {
                case EntityStatusConsts.WarningOne:
                    await ManagerAppService.WarnFirstAsync(id);
                    break;
                case EntityStatusConsts.WarningTwo:
                    await ManagerAppService.WarnSecondAsync(id);
                    break;
                case EntityStatusConsts.Blocked:
                    await ManagerAppService.BlockAsync(id);
                    break;
                case "unblocked":
                    await ManagerAppService.UnblockAsync(id);
                    break;
                default:
                    throw new BusinessException("Invalid manager status");
            }

            return await ManagerAppService.GetAsync(id);
        }


    }
}
