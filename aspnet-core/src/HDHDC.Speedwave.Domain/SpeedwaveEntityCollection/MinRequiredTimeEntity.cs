using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class MinRequiredTimeEntity : Entity<int>, ICreationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public MinRequiredTimeEntity()
        {
        }        

        public TimeSpan MinRequiredTime { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }
        #endregion
    }
}
