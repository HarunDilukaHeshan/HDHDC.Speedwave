using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class CancelledOrderEntity : Entity<int>, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject
    {
        public CancelledOrderEntity(int id)
        {
            this.Id = id;
        }

        public int CancellationReasonId { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; } = false;
        public int? ApproverId { get; set; }
        public CancellationReasonEntity CancellationReasonEntity { get; set; }
        public OrderEntity OrderEntity { get; set; }
        public ManagerEntity ManagerEntity { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
    }
}
