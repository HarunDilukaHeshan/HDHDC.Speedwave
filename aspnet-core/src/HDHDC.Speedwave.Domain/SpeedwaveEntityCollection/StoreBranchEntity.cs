using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class StoreBranchEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public StoreBranchEntity()
            : base()
        { }        
        public int StoreChainID { get; protected set; }
        public int CityID { get; protected set; }
        public string Geolocation { get; protected set; }
        public string ContactNo01 { get; protected set; }
        public string ContactNo02 { get; protected set; }
        public CityEntity CityEntity { get; set; }
        public StoreChainEntity StoreChainEntity { get; set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
