using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    /// <summary>
    /// 
    /// </summary>
    public class AddressEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IHasDeletionTime, IHasConcurrencyStamp
    {
        public AddressEntity(string addressLine, int cityID, int customerID, string geolocation)
            : base()
        {
            AddressLine = addressLine;
            CityID = cityID;
            CustomerID = customerID;
            Geolocation = geolocation;
        }

        protected AddressEntity()
        { }

        public string AddressLine { get; set; }
        public int CityID { get; set; }
        public int CustomerID { get; set; }
        /// <summary>
        /// This property has a pre specified format
        /// [latitude:longitude]
        /// [6.456578:79.5465125]
        /// </summary>
        public string Geolocation { get; set; }
        public string Note { get; set; }
        public CustomerEntity CustomerEntity { get; protected set; }
        public CityEntity CityEntity { get; set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
