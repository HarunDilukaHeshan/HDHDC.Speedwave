using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.Controllers
{
    [Route("apiv1/[controller]")]
    public class UserController : SpeedwaveController
    {
        private IUserAppService UserAppService { get; }

        public UserController(IUserAppService userAppService)
        {
            UserAppService = userAppService;
        }

        [HttpGet("current")]
        public async Task<AppUserDto> Get()
        {
            return await UserAppService.GetCurrentUserAsync();
        }

        [HttpGet("username/{username}")]
        public async Task Get([FromRoute]string username)
        {
            await UserAppService.IsUsernameAvailable(username);
        }

        [HttpPost("password-reset-code")]
        public async Task SendPasswordResetCode([FromBody]PasswordResetDto dto)
        {
            await UserAppService.SendPasswordResetCodeAsync(dto.UserName);
        }

        [HttpPost("password-reset")]
        public async Task ResetPassword([FromBody]PasswordResetDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PasswordResetToken) || string.IsNullOrWhiteSpace(dto.NewPassword))
                throw new BusinessException();

            await UserAppService.ResetPasswordAsync(dto.UserName, dto.PasswordResetToken, dto.NewPassword);
        }
    }
}

