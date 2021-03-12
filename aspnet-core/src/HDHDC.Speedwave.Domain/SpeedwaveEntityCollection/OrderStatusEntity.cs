using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class OrderStatusEntity
        : Entity<string>, ICreationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public OrderStatusEntity(string id)
            : base(id)
        { }

        protected OrderStatusEntity()
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
