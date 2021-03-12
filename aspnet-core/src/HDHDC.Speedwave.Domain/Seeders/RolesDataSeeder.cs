using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.Seeders
{
    public class RolesDataSeeder
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IGuidGenerator _guidGenerator;

        public RolesDataSeeder(
            IIdentityRoleRepository identityRoleRepository, 
            IGuidGenerator guidGenerator)
        {
            _identityRoleRepository = identityRoleRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _identityRoleRepository.GetCountAsync() > 1) return;

            await _identityRoleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), RolesConsts.SystemAdmin) { }, autoSave: true);

            await _identityRoleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), RolesConsts.SuperAdmin) { }, autoSave: true);

            await _identityRoleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), RolesConsts.Manager) { }, autoSave: true);

            await _identityRoleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), RolesConsts.Rider) { }, autoSave: true);

            await _identityRoleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), RolesConsts.Customer) { }, autoSave: true);
        }
    }
}
