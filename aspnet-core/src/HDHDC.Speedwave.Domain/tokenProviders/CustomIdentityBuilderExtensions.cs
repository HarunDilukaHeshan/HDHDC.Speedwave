using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDHDC.Speedwave.tokenProviders
{
    public static class CustomIdentityBuilderExtensions
    {
        public static IdentityBuilder AddPasswordResetTotpTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var totpProvider = typeof(PasswordResetTotpTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("PasswordResetTotpTokenProvider", totpProvider);
        }
    }
}
