using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class ItemStoreBranchEntity
        : Entity, IMustHaveCreator, IHasCreationTime
    {
        public ItemStoreBranchEntity(int itemID, int storeBranchID)
            : base()
        {
            ItemID = itemID;
            StoreBranchID = storeBranchID;
        }

        protected ItemStoreBranchEntity()
        { }

        public int ItemID { get; protected set; }
        public int StoreBranchID { get; protected set; }        
        public ItemEntity ItemEntity { get; set; }
        public StoreBranchEntity StoreBranchEntity { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { ItemID, StoreBranchID };
        }

        #region audited_properties
        public DateTime CreationTime { get; set; }
        public Guid CreatorId { get; set; }
        #endregion
    }
}
