using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class ItemCategoryEntity
        : Entity, IMustHaveCreator, IHasCreationTime
    {
        public ItemCategoryEntity(int itemID, int categoryID)
            : base()
        {
            ItemID = itemID;
            CategoryID = categoryID;
        }

        protected ItemCategoryEntity()
        { }

        public int ItemID { get; protected set; }
        public int CategoryID { get; protected set; }
        public ItemEntity ItemEntity { get; protected set; }
        public CategoryEntity CategoryEntity { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { ItemID, CategoryID };
        }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        #endregion
    }
}
