using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class DistrictEntity
        : Entity<string>, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public DistrictEntity(string id, string provinceID)
            : base(id)
        {
            ProvinceID = provinceID;
        }
        protected DistrictEntity()
        { }

        public string ProvinceID { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
