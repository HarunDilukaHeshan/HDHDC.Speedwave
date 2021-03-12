using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class PaymentEntity 
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public PaymentEntity(int orderID, int deliveryChargeID, float nettotal, float subtotal)
            : base()
        {
            OrderID = orderID;
            DeliveryChargeID = deliveryChargeID;
            Nettotal = nettotal;
            Subtotal = subtotal;
        }

        protected PaymentEntity() 
        { }

        public int DeliveryChargeID { get; protected set; }
        public int OrderID { get; protected set; }
        public float Nettotal { get; protected set; }
        public float Subtotal { get; protected set; }
        public float TotalPaid { get; set; }
        public string PaymentStatus { get; internal set; } = PaymentStatusConsts.Pending;
        public IList<PaymentDetailEntity> PaymentDetailEntities { get; protected set; }
        public DeliveryChargeEntity DeliveryChargeEntity { get; protected set; }
        public OrderEntity OrderEntity { get; protected set; }
        public string ConcurrencyStamp { get; set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set ; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
