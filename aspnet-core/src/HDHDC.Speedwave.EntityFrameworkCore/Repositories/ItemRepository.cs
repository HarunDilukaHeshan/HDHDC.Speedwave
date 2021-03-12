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
using HDHDC.Speedwave.Options;
using Microsoft.Extensions.Options;
using Volo.Abp;

namespace HDHDC.Speedwave.Repositories
{
    public class ItemRepository : EfCoreRepository<SpeedwaveDbContext, ItemEntity, int>, IItemRepository
    {
        protected GeoDistanceOptions GeoDistanceOptions { get; }

        public ItemRepository(
            IOptions<GeoDistanceOptions> geoDistanceOptions,
            IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        {
            GeoDistanceOptions = geoDistanceOptions.Value;
        }

        public async Task<ItemEntity[]> SearchAsync(string keywords = "", int skipCount = 0, int maxResultCount = 10)
        {
            keywords = keywords.Trim();
            var keywordsParam = new SqlParameter("@keywords", keywords);

            return await DbContext.ItemEntities
                .FromSqlRaw("SELECT ie.* FROM Speedy.searchForItems({0}, {1}, {2}) AS t1 INNER JOIN Speedy.ItemEntity AS ie ON t1.Id = ie.Id", keywordsParam, skipCount, maxResultCount)
                .ToArrayAsync();
        }

        public async Task<ItemEntity[]> SelectItemsAsync(int seed, int maxResultCount = 10)
        {
            var rnd = (seed == 0)? new Random() : new Random(seed);
            var count = await DbContext.ItemEntities.CountAsync();            
            var skipCount = rnd.Next(0, count - 1);            

            var entityArr = await DbContext.ItemEntities
                .FromSqlRaw("SELECT ie.* FROM Speedy.searchForItems({0}, {1}, {2}) AS t1 INNER JOIN Speedy.ItemEntity AS ie ON t1.Id = ie.Id", "", skipCount, maxResultCount)
                .OrderBy(e => e.ItemDescription)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToArrayAsync();

            return entityArr;
        }

        public async Task<ItemEntity[]> SearchWithinTheRadiusAsync(
            int cityId,
            string keywords = "", 
            int skipCount = 0, 
            int maxResultCount = 10)
        {
            var radius = GeoDistanceOptions.MaxRadius;

            var itemEsArr = await DbContext.ItemEntities
                .FromSqlRaw(
                "SELECT iR.* FROM [Speedy].[searchItemsWithinRadius]({0}, {1}, {2}, {2}) siR INNER JOIN [Speedy].[ItemEntity] iR ON siR.Id = iR.Id",
                cityId, radius, keywords, skipCount, maxResultCount)
                .ToArrayAsync();

            return itemEsArr;
        }

        public async Task<ItemEntity[]> RndSelectWithinTheRadiusAsync(
            int cityId,
            int seed, 
            int maxResultCount = 10)
        {
            var rnd = (seed == 0) ? new Random() : new Random(seed);
            var count = await DbContext.ItemEntities.CountAsync();
            var skipCount = rnd.Next(0, (count > 4)? count - 4 : count - 1);

            var radius = GeoDistanceOptions.MaxRadius;

            var itemEsArr = await DbContext.ItemEntities
                .FromSqlRaw(
                "SELECT iR.* FROM [Speedy].[searchItemsWithinRadius]({0}, {1}, {2}, {2}) siR INNER JOIN [Speedy].[ItemEntity] iR ON siR.Id = iR.Id",
                cityId, radius, "", skipCount, maxResultCount)
                .ToArrayAsync();

            return itemEsArr;
        }

        public async Task<bool> ValidateItemsWithinRadiusAsync(int cityId, int[] itemIds)
        {
            var cityE = await DbContext.CityEntities.FirstOrDefaultAsync(e => e.Id == cityId);
            var geoL = cityE.Geolocation.Split(':');

            var sbArr = await DbContext.StoreBranchEntities
                .FromSqlRaw("SELECT sbR.* FROM [Speedy].[getStoreBranches02]({0}, {1}, {2}) sbfR INNER JOIN [Speedy].[StoreBranchEntity] sbR ON sbfR.Id = sbR.Id",
                geoL[0], geoL[1], GeoDistanceOptions.MaxRadius)
                .ToArrayAsync();

            var hasFound = false;

            foreach (var itemId in itemIds)
            {                
                foreach (var sbR in sbArr)
                {
                    hasFound = (DbContext.ItemStoreBranchEntities
                        .SingleOrDefaultAsync(e => e.ItemID == itemId && e.StoreBranchID == sbR.Id) != null);
                    if (hasFound) break;
                }

                if (!hasFound) break;
            }

            return hasFound;
        }
    }
}
