using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class DeliveryChargeEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IHasModificationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public DeliveryChargeEntity(
            float charge,
            int distanceChargeID, 
            int subtotalPercentageID, 
            int deliveryScheduleID)
            : base()
        {            
            DistanceChargeID = distanceChargeID;            
            SubtotalPercentageID = subtotalPercentageID;
            DeliveryScheduleID = deliveryScheduleID;
            Charge = charge;
        }
        protected DeliveryChargeEntity()
        { }

        public int DistanceChargeID { get; protected set; }
        public int SubtotalPercentageID { get; protected set; }
        public int DeliveryScheduleID { get; protected set; }
        public float Charge { get; protected set; }
        public DeliveryScheduleEntity DeliveryScheduleEntity { get; protected set; }
        public DistanceChargeEntity DistanceChargeEntity { get; protected set; }
        public SubtotalPercentageEntity SubtotalPercentageEntity { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeleterId { get; set; }
        #endregion
    }
}
