using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace HDHDC.Speedwave.Dependencies
{
    public class StoreBranchSelector
    {
        public StoreBranchSelector(
            IRepository<StoreBranchEntity, int> storeBranchRepository,
            IAsyncQueryableExecuter asyncExecutor)
        {
            StoreBranchRepository = storeBranchRepository;
            AsyncExecutor = asyncExecutor;
        }

        protected IRepository<StoreBranchEntity, int> StoreBranchRepository { get; }
        protected IAsyncQueryableExecuter AsyncExecutor { get; }

        public async Task<IList<StoreBranchEntity>> GetStoreBranches(IList<CityEntity> cityEntities)
        {
            var storeBranchesList = new List<StoreBranchEntity>();

            foreach(var cityE in cityEntities)
            {
                var branchEsList = await AsyncExecutor.ToListAsync(StoreBranchRepository.Where(e => e.CityID == cityE.Id));
                storeBranchesList.AddRange(branchEsList);
            }            

            return storeBranchesList;
        }
    }
}
