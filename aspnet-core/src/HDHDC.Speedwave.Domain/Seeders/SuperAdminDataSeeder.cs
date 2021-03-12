using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.Seeders
{
    public class SuperAdminDataSeeder : IDataSeedContributor, ITransientDependency
    {
        protected IdentityUserManager IdentityUserManager { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public SuperAdminDataSeeder(
            IGuidGenerator guidGenerator,
            IdentityUserManager identityUserManager)
        {
            IdentityUserManager = identityUserManager;
            GuidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var userName = "superadmin01";
            var email = userName + "@speedy.com";
            var phoneNo = "0750000000";

            if ((await IdentityUserManager.FindByNameAsync(userName)) != null) return;

            var userE = new IdentityUser(GuidGenerator.Create(), userName, email)
            {
                Name = userName,
                Surname = userName
            };

            await IdentityUserManager.CreateAsync(userE, "!QAZ2wsx");

            await IdentityUserManager.SetEmailAsync(userE, email);
            await IdentityUserManager.SetPhoneNumberAsync(userE, phoneNo);

            await IdentityUserManager.AddClaimAsync(userE, new System.Security.Claims.Claim("sub", userE.Id.ToString()));

            await IdentityUserManager.AddToRoleAsync(userE, RolesConsts.SuperAdmin);
        }
    }
}
