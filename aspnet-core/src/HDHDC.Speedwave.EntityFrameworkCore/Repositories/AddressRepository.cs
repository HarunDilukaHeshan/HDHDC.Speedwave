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
using Volo.Abp;
using System.Threading;

namespace HDHDC.Speedwave.Repositories
{
    public class AddressRepository : EfCoreRepository<SpeedwaveDbContext, AddressEntity, int>, IAddressRepository
    {
        public AddressRepository(IDbContextProvider<SpeedwaveDbContext> provider)
             : base(provider)
        { }

        public virtual async Task<AddressEntity[]> GetAllAsync(Guid userId)
        {
            var addressList = await (from addressE in DbContext.AddressEntities
                            join customerE in DbContext.CustomerEntities on addressE.CustomerID equals customerE.Id
                            join userE in DbContext.Users on customerE.UserID equals userE.Id
                            select addressE)
                            .Include(e => e.CityEntity)
                            .ToArrayAsync();

            return addressList;
        }

        public virtual async Task<AddressEntity> GetAsync(Guid userId, int id)
        {
            var addressE = await (from addrE in DbContext.AddressEntities
                            join customerE in DbContext.CustomerEntities on addrE.CustomerID equals customerE.Id
                            join userE in DbContext.Users on customerE.UserID equals userE.Id
                            where addrE.Id == id
                            select addrE)
                            .SingleOrDefaultAsync();
            return addressE;
        }

        public virtual async Task<AddressEntity> InsertAsync(Guid userId, AddressEntity entity)
        {
            var customerE = await DbContext.CustomerEntities.SingleOrDefaultAsync(e => e.UserID == userId)
                ?? throw new BusinessException();
            entity.CustomerID = customerE.Id;
            DbContext.AddressEntities.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<AddressEntity> UpdateAsync(Guid userId, int addressId, AddressEntity entity)
        {
            var customerE = await DbContext.CustomerEntities.SingleOrDefaultAsync(e => e.UserID == userId)
                ?? throw new BusinessException();
            var addrE = await DbContext.AddressEntities
                .SingleOrDefaultAsync(e => e.Id == addressId && e.CustomerID == customerE.Id) ?? throw new BusinessException();

            addrE.AddressLine = entity.AddressLine;
            addrE.Geolocation = entity.Geolocation;
            addrE.Note = entity.Note;

            return await base.UpdateAsync(addrE, autoSave: true);
        }

        public virtual async Task DeleteAsync(Guid userId, int addressId)
        {
            var customerE = await DbContext.CustomerEntities.SingleOrDefaultAsync(e => e.UserID == userId)
                ?? throw new BusinessException();
            var addrE = await DbContext.AddressEntities
                .SingleOrDefaultAsync(e => e.Id == addressId && e.CustomerID == customerE.Id) ?? throw new BusinessException();

            await base.DeleteAsync(addrE, autoSave: true);
        }
    }
}
