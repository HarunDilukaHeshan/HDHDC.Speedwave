using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class QuantityEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {        
        public QuantityEntity()
        { }

        public float Quantity { get; set; }
        public string UnitID { get; set; }
        public string NormalizedQuantityLabel { get; set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
