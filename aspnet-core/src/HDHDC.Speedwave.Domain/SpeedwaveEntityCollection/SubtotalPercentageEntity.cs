using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class SubtotalPercentageEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public SubtotalPercentageEntity(uint percentage, float from)
            : base()
        {
            Percentage = percentage;
            From = from;
        }

        protected SubtotalPercentageEntity()
        { }

        public uint Percentage { get; protected set; }
        public float From { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
