using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IItemStoreBranchRepository : IRepository<ItemStoreBranchEntity>
    {
        Task<ItemStoreBranchEntity[]> GetListAsync(
            Expression<Func<ItemStoreBranchEntity, bool>> predicate, 
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false);

        Task<ItemStoreBranchEntity[]> SearchAsync(
            string keywords,
            int branchId,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false);
    }
}
