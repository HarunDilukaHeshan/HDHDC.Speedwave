using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class ManagerEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public ManagerEntity(Guid userID, string districtID)
            : base()
        {
            UserID = userID;
            DistrictID = districtID;
        }        
        public Guid UserID { get; protected set; }
        public string DistrictID { get; set; }
        public string Status { get; set; } = EntityStatusConsts.Active;
        public AppUser AppUser { get; set; }
        public DistrictEntity DistrictEntity { get; protected set; }
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
