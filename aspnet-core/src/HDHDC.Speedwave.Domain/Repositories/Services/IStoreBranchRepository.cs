using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IStoreBranchRepository : IRepository<StoreBranchEntity, int>
    {
        Task<StoreBranchEntity[]> GetAllBranchAroundTheCityAsync(int cityId);
    }
}
