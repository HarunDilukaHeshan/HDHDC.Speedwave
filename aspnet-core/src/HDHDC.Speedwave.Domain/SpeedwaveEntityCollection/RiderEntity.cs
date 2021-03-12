using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class RiderEntity
        : Entity<int>, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public RiderEntity(Guid userID, string geolocation, int cityID)
            : base()
        {
            UserID = userID;
            Geolocation = geolocation;
            CityID = cityID;
        }

        public Guid UserID { get; protected set; }
        public string Geolocation { get; set; }
        public int CityID { get; set; }
        public string Status { get; set; } = EntityStatusConsts.Active;
        public AppUser AppUser { get; set; }
        public CityEntity CityEntity { get; set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
