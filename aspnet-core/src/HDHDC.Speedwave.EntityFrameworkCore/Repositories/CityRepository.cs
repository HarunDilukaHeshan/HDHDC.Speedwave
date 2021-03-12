using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HDHDC.Speedwave.Repositories
{
    public class CityRepository : EfCoreRepository<SpeedwaveDbContext, CityEntity, int>, ICityRepository
    {
        public CityRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }

        public async Task<CityEntity[]> SearchAsync(int skipCount = 0, int maxResultCount = 10, string keyword = "")
        {
            CityEntity[] cEs;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                cEs = await (from cityE in DbContext.CityEntities
                                select cityE)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .ToArrayAsync();
            }
            else
            {
                cEs = await (from cityE in DbContext.CityEntities
                             where cityE.CityName == "%" + keyword + "%"
                             select cityE)
                             .Skip(skipCount)
                             .Take(maxResultCount)
                             .ToArrayAsync();
            }            

            return cEs;
        }

        public async Task<CityEntity[]> GetListAsync(string districtId)
        {
            var cEs = await DbContext.CityEntities.Where(e => e.DistrictID == districtId)
                .ToArrayAsync();

            return cEs;
        }

        public async Task<CityEntity[]> GetCityListWithinTheRadiusAsync(int cityId, float radius)
        {
            var cEs = await DbContext.CityEntities.FromSqlRaw(@"SELECT * FROM [Speedy].[CityEntity] AS cityR INNER JOIN [Speedy].[getCitiesWithinTheRadius]({0}, {1}) AS cIdR on cityR.Id = cIdR.Id", cityId, radius)
                .ToArrayAsync();

            return cEs;
        }
    }
}
