using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.DTOs
{
    public class CustomerDto : EntityDto<int>, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public Guid UserID { get; protected set; }
        public string Status { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }
        #endregion
    }
}
