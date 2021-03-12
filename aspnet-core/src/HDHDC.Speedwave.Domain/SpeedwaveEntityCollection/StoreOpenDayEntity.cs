using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class StoreOpenDayEntity
        : Entity, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IHasConcurrencyStamp
    {
        public StoreOpenDayEntity(int storeBranchID, DayOfWeek dayOfWeek)
        {
            StoreBranchID = storeBranchID;
            DayOfWeek = dayOfWeek;
        }
        protected StoreOpenDayEntity()
        { }

        public int StoreBranchID { get; protected set; }
        public DayOfWeek DayOfWeek { get; protected set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }        
        public StoreBranchEntity StoreBranchEntity { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { StoreBranchID, DayOfWeek };
        }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        #endregion
    }
}
