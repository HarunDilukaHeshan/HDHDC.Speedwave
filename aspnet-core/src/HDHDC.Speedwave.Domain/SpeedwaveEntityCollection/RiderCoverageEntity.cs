using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class RiderCoverageEntity
        : Entity, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public RiderCoverageEntity(int riderID, int cityID)
        {
            RiderID = riderID;
            CityID = cityID;
        }

        public int RiderID { get; protected set; }
        public int CityID { get; protected set; }       
        public RiderEntity RiderEntity { get; protected set; }
        public CityEntity CityEntity { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { RiderID, CityID };
        }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
