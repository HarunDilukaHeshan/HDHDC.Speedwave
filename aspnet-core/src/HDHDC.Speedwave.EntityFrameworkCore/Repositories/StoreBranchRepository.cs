using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Options;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class StoreBranchRepository : EfCoreRepository<SpeedwaveDbContext, StoreBranchEntity, int>, IStoreBranchRepository
    {
        protected GeoDistanceOptions GeoDistanceOptions { get; }

        public StoreBranchRepository(
            IOptions<GeoDistanceOptions> geoOptions,
            IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        {
            GeoDistanceOptions = geoOptions.Value;
        }

        public async Task<StoreBranchEntity[]> GetAllBranchAroundTheCityAsync(int cityId)
        {
            var cityE = await DbContext.CityEntities.SingleOrDefaultAsync(e => e.Id == cityId)
                ?? throw new BusinessException();

            var geoL = cityE.Geolocation.Split(':');

            var sbEs = await DbContext.StoreBranchEntities
                .FromSqlRaw("SELECT sbR.* FROM [Speedy].[StoreBranchEntity] sbR " +
                " INNER JOIN [Speedy].[getStoreBranches02]({0}, {1}, {2}) bR ON sbR.Id = bR.Id",
                geoL[0], 
                geoL[1], 
                GeoDistanceOptions.MaxRadius)
                .ToArrayAsync();

            return sbEs;
        }
    }
}
