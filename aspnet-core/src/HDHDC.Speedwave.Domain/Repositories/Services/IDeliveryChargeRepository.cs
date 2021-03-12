using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IDeliveryChargeRepository : IRepository<DeliveryChargeEntity, int>
    {
        Task<RiderMeanDistanceKeylessEntity> GetMeanDistanceAsync(int addressId);
    }
}
