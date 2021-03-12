using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class PaymentStatusEntity
        : Entity<string>, ICreationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public PaymentStatusEntity(string id)
            : base(id)
        { }
        protected PaymentStatusEntity()
        { }

        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
