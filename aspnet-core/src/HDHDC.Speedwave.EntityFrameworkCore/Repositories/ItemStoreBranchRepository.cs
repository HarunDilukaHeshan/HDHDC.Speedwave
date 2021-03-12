using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class ItemStoreBranchRepository : EfCoreRepository<SpeedwaveDbContext, ItemStoreBranchEntity>, IItemStoreBranchRepository
    {
        public ItemStoreBranchRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }

        public virtual async Task<ItemStoreBranchEntity[]> GetListAsync(
            Expression<Func<ItemStoreBranchEntity, bool>> predicate,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false)
        {
            ItemStoreBranchEntity[] entityArr = new ItemStoreBranchEntity[0];

            if (maxResultCount == 0)
                entityArr = await DbContext.ItemStoreBranchEntities
                    .Where(predicate)
                    .IncludeIf(includeDetails, e => e.ItemEntity)
                    .ToArrayAsync();
            else
                entityArr = await DbContext.ItemStoreBranchEntities
                    .Where(predicate)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .IncludeIf(includeDetails, e => e.ItemEntity)
                    .ToArrayAsync();            

            return entityArr;
        }

        public virtual async Task<ItemStoreBranchEntity[]> SearchAsync(
            string keywords,
            int branchId,
            int skipCount = 0,
            int maxResultCount = 10,
            bool includeDetails = false)
        {
            var itemBranchList = new List<ItemStoreBranchEntity>();
            var arr = await (from itemBranchE in DbContext.ItemStoreBranchEntities
                             join itemE in DbContext.ItemEntities on itemBranchE.ItemID equals itemE.Id
                             where (EF.Functions.Like(itemE.ItemName, "%" + keywords + "%") ||
                             EF.Functions.Like(itemE.ItemDescription, "%" + keywords + "%")) &&
                             itemBranchE.StoreBranchID == branchId
                             select new { itemE, itemBranchE })
                      .OrderBy(e => e.itemBranchE.StoreBranchID)
                      .Skip(skipCount)
                      .Take(maxResultCount)
                      .ToArrayAsync();

            foreach (var e in arr)
                itemBranchList.Add(new ItemStoreBranchEntity(e.itemE.Id, branchId) { 
                    ItemEntity = (includeDetails)? e.itemE : null
                });

            return itemBranchList.ToArray();
        }
    }
}
