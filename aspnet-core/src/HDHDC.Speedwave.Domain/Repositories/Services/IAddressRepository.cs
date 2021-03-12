using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Repositories.Services
{
    public interface IAddressRepository : IRepository<AddressEntity, int>
    {
        Task<AddressEntity[]> GetAllAsync(Guid userId);
        Task<AddressEntity> InsertAsync(Guid userId, AddressEntity entity);
        Task<AddressEntity> UpdateAsync(Guid userId, int addressId, AddressEntity entity);
        Task DeleteAsync(Guid userId, int addressId);
        Task<AddressEntity> GetAsync(Guid userId, int id);
    }
}
