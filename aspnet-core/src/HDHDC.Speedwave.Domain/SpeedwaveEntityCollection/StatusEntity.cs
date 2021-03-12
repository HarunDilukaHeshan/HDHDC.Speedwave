using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class StatusEntity
        : Entity<string>, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public StatusEntity(string id)
            : base(id)
        { }
        protected StatusEntity()
        { }

        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
