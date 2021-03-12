using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.DTOs
{
    public class ManagerDto : EntityDto<int>, 
        IMustHaveCreator, 
        IHasCreationTime, 
        IModificationAuditedObject, 
        IDeletionAuditedObject, 
        IHasConcurrencyStamp
    {
        public Guid UserID { get; protected set; }
        public string DistrictID { get; protected set; }
        public string Status { get; set; }
        public AppUserDto AppUser { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
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
