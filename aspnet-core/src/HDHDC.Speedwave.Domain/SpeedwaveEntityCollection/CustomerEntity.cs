using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class CustomerEntity
        : Entity<int>, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public CustomerEntity(Guid userID)
            : base()
        {
            UserID = userID;
        }
        protected CustomerEntity()
        { }
        public Guid UserID { get; protected set; }
        public string Status { get; set; } = EntityStatusConsts.Active;
        
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        #endregion
    }
}
