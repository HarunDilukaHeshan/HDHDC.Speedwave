using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IUserAppService : IApplicationService
    {
        Task<AppUserDto> GetCurrentUserAsync();
        Task<bool> IsUsernameAvailable(string username);
        Task<AppUserDto> GetByUsername(string username);
        Task<AppUserDto> GetAsync(Guid userId);
        Task SendPasswordResetCodeAsync(string userName);
        Task ResetPasswordAsync(string userName, string resetCode, string password);
    }
}
