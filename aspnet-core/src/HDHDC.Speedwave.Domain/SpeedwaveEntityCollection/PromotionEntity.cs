using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class PromotionEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        /// <summary>
        /// Promotion that can be used many times
        /// </summary>
        /// <param name="id"></param>
        /// <param name="noOfTimes"></param>
        /// <param name="expireDate"></param>
        public PromotionEntity(uint noOfTimes, DateTime expireDate)
            : this(false, noOfTimes, expireDate)
        { }

        /// <summary>
        /// Promotion that can only be used once
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expireDate"></param>
        public PromotionEntity(DateTime expireDate)
            : this(true, 0, expireDate)
        { }

        private PromotionEntity(bool isOneTime, uint noOfTimes, DateTime expireDate)
            : base()
        {
            IsOneTime = isOneTime;
            NoOfTimes = noOfTimes;
            ExpireDate = expireDate;
        }

        protected PromotionEntity()
        { }

        public bool IsOneTime { get; protected set; }
        public uint NoOfTimes { get; set; }
        public DateTime ExpireDate { get; protected set; }
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
