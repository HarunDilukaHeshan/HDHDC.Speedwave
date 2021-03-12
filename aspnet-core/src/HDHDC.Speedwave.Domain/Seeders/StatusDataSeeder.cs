using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Seeders
{
    public class StatusDataSeeder
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<StatusEntity, string> _statusRepository;

        public StatusDataSeeder(IRepository<StatusEntity, string> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _statusRepository.GetCountAsync() > 0) return;

            await _statusRepository.InsertAsync(new StatusEntity(EntityStatusConsts.Active), autoSave:true);
            await _statusRepository.InsertAsync(new StatusEntity(EntityStatusConsts.Inactive), autoSave: true);
            await _statusRepository.InsertAsync(new StatusEntity(EntityStatusConsts.WarningOne), autoSave: true);
            await _statusRepository.InsertAsync(new StatusEntity(EntityStatusConsts.WarningTwo), autoSave: true);
            await _statusRepository.InsertAsync(new StatusEntity(EntityStatusConsts.Blocked), autoSave: true);
        }
    }
}
