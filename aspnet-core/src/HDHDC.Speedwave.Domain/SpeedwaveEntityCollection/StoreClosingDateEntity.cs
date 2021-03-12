using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class StoreClosingDateEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public StoreClosingDateEntity(int storeBranchID, DateTime closingDate)
            : base()
        {
            StoreBranchID = storeBranchID;
            ClosingDate = closingDate;
        }

        protected StoreClosingDateEntity()
        { }

        public int StoreBranchID { get; protected set; }
        public DateTime ClosingDate { get; protected set; }
        public StoreBranchEntity StoreBranchEntity { get; protected set; }
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
