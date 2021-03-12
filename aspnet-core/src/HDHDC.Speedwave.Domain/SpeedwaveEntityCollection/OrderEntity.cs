using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class OrderEntity 
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public OrderEntity(int addressId, int deliveryScheduleID, int? promotionID = null)
            : base()
        {
            DeliveryScheduleID = deliveryScheduleID;
            PromotionID = promotionID;
            AddressID = addressId;
        }

        protected OrderEntity()
        { }

        public int DeliveryScheduleID { get; protected set; }
        public int AddressID { get; protected set; }
        public int? PromotionID { get; protected set; }
        public int? RiderID { get; set; }
        public string OrderStatus { get; set; } = OrderStatusConsts.Pending;
        public PaymentEntity PaymentEntity { get; protected set; }
        public DeliveryScheduleEntity DeliveryScheduleEntity { get; protected set; }
        public AddressEntity AddressEntity { get; protected set; }
        public PromotionEntity PromotionEntity { get; protected set; }
        public RiderEntity RiderEntity { get; protected set; }
        public IList<OrderItemEntity> OrderItemEntities { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeleterId { get; set; }
        #endregion
    }
}
