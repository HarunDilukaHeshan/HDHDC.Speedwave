using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.tokenProviders
{
    public static class CustomIdentityUserManagerExtensions
    {
        public static async Task<string> GeneratePasswordResetTotpTokenAsync(this IdentityUserManager userManager, IdentityUser user)
        {
            _ = user ?? throw new ArgumentNullException();            

           return await userManager.GenerateUserTokenAsync(user, "PasswordResetTotpTokenProvider", "PasswordReset");
        }

        public static async Task<bool> VerifyPasswordResetTotpTokenAsync(this IdentityUserManager userManager, IdentityUser user, string token)
        {
            _ = user ?? throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException();

            return await userManager.VerifyUserTokenAsync(user, "PasswordResetTotpTokenProvider", "PasswordReset", token);
        }
    }
}
