using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedy.SpeedyAppServices
{
    public class RiderCoverageAppService
        : AbstractKeyCrudAppService<RiderCoverageEntity, RiderCoverageDto, RiderCoverageKey>
    {
        public RiderCoverageAppService(IRepository<RiderCoverageEntity> repository)
            : base(repository)
        { }

        protected override async Task DeleteByIdAsync(RiderCoverageKey id)
        {
            await Repository.DeleteAsync(e => e.RiderID == id.RiderID && e.CityID == id.CityID);
        }

        protected override async Task<RiderCoverageEntity> GetEntityByIdAsync(RiderCoverageKey id)
        {
            return await Repository.GetAsync(e => e.RiderID == id.RiderID && e.CityID == id.CityID);
        }
    }

    public class RiderCoverageKey
    {
        public int RiderID { get; set; }
        public int CityID { get; set; }
    }
}
