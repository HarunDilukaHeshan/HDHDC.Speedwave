using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IItemCategoryRepository : IRepository<ItemCategoryEntity>
    {
        Task<IList<ItemCategoryEntity>> GetListAsync(Expression<Func<ItemCategoryEntity, bool>> func);
    }
}
