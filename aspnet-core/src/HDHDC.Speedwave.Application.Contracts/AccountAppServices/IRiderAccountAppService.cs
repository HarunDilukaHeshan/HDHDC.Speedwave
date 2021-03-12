using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.AccountAppServices
{
    public interface IRiderAccountAppService : IApplicationService, ITransientDependency
    {
        Task<AppUserDto> RegisterAsync(UserCreateDto input, RiderCreateDto riderCreateDto);
    }
}
