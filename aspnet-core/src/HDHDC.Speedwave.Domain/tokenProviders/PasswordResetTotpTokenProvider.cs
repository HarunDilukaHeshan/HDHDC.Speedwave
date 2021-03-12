using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDHDC.Speedwave.tokenProviders
{
    public class PasswordResetTotpTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
        where TUser : class
    {
        public PasswordResetTotpTokenProvider() : base()
        { 
        }        

        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            if (manager == null || user == null) throw new ArgumentNullException();

            return await Task.FromResult(false);
        }

        public override async Task<string> GetUserModifierAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentException();
            if (manager == null || user == null) throw new ArgumentNullException();

            var userId = await manager.GetUserIdAsync(user);
            return "PasswordChangeTotpToken:" + purpose + ":" + userId.ToString();
        }
    }
}
