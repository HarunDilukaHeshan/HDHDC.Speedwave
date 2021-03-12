using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class DistanceChargeEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public DistanceChargeEntity(float charge, uint from)
            : base()
        {
            Charge = charge;
            From = from;
        }

        protected DistanceChargeEntity()
        { }

        public float Charge { get; protected set; }
        public uint From { get; protected set; }        

        #region audited_properties
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        public string ConcurrencyStamp { get; set; }
        #endregion
    }
}
