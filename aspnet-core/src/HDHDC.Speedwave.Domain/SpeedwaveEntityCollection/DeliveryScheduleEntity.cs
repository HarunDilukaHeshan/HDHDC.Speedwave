using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class DeliveryScheduleEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public DeliveryScheduleEntity(TimeSpan timePeriod, float costIncreasePercentage)
            : base()
        {
            TimePeriod = timePeriod;
            CostIncreasePercentage = costIncreasePercentage;
        }

        protected DeliveryScheduleEntity()
        { }

        public string DeliveryScheduleName { get; set; }
        public TimeSpan TimePeriod { get; protected set; }
        public float CostIncreasePercentage { get; protected set; }
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
