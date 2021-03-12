using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.tokenProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository IdentityUserRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }
        protected IEmailSender EmailSender { get; }

        public UserAppService(
            IdentityUserManager userManager,
            IEmailSender emailSender,
            ILookupNormalizer lookupNormalizer,
            IIdentityUserRepository identityUserRepository)
        {
            IdentityUserRepository = identityUserRepository;
            LookupNormalizer = lookupNormalizer;
            UserManager = userManager;
            EmailSender = emailSender;
        }

        public async Task<AppUserDto> GetAsync(Guid userId)
        {
            var userE = await IdentityUserRepository.GetAsync(userId);
            return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, AppUserDto>(userE);
        }

        public async Task<AppUserDto> GetCurrentUserAsync()
        {
            var userId = CurrentUser.Id ?? throw new BusinessException();
            var userE = await IdentityUserRepository.GetAsync(userId);
            return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, AppUserDto>(userE);
        }

        public async Task<bool> IsUsernameAvailable(string username)
        {            
            return (await IdentityUserRepository.FindByNormalizedUserNameAsync(LookupNormalizer.NormalizeName(username))) == null;
        }

        public async Task<AppUserDto> GetByUsername(string username)
        {
            var userE = await IdentityUserRepository.FindByNormalizedUserNameAsync(LookupNormalizer.NormalizeName(username));
            return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, AppUserDto>(userE);
        }

        public async Task SendPasswordResetCodeAsync(string userName)
        {            
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Invalid userName");
            
            var userE = await UserManager.FindByNameAsync(LookupNormalizer.NormalizeName(userName)) ?? throw new EntityNotFoundException();

            _ = await UserManager.UpdateSecurityStampAsync(userE);
            var token = await UserManager.GeneratePasswordResetTotpTokenAsync(userE);

            await EmailSender.SendAsync(userE.Email, "Password reset code", token);
        }

        public async Task ResetPasswordAsync(string userName, string resetCode, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || 
                string.IsNullOrWhiteSpace(resetCode) || 
                string.IsNullOrWhiteSpace(password)) throw new ArgumentException();

            var userE = await UserManager.FindByNameAsync(LookupNormalizer.NormalizeName(userName)) ?? throw new EntityNotFoundException();
            if (!(await UserManager.VerifyPasswordResetTotpTokenAsync(userE, resetCode))) throw new BusinessException();

            await UserManager.RemovePasswordAsync(userE);
            await UserManager.AddPasswordAsync(userE, password);
            await UserManager.UpdateSecurityStampAsync(userE);
        }
    }
}
