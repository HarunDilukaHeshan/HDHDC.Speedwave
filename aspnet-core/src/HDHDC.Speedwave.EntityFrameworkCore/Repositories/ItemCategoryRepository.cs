using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace HDHDC.Speedwave.Repositories
{
    public class ItemCategoryRepository : EfCoreRepository<SpeedwaveDbContext, ItemCategoryEntity>, IItemCategoryRepository
    {
        public ItemCategoryRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }

        public async Task<IList<ItemCategoryEntity>> GetListAsync(Expression<Func<ItemCategoryEntity, bool>> func)
        {
            return await DbContext.ItemCategoryEntities
                .Where(func)
                .ToListAsync();
        }
    }
}
