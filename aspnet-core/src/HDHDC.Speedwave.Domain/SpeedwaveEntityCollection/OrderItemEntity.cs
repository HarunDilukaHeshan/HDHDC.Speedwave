using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class OrderItemEntity
        : Entity, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public OrderItemEntity(int orderID, int itemID, uint quantity, float itemPrice)
        {
            OrderID = orderID;
            ItemID = itemID;
            Quantity = quantity;
            ItemPrice = itemPrice;
        }

        protected OrderItemEntity()
        { }

        public int OrderID { get; protected set; }
        public int ItemID { get; protected set; }
        public float ItemPrice { get; protected set; }        
        public uint Quantity { get; protected set; }        
        public ItemEntity ItemEntity { get; protected set; }
        public OrderEntity OrderEntity { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { OrderID, ItemID };
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
