using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class PaymentDetailEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public PaymentDetailEntity(int paymentId, string paymentMethod, float paidAmount)
            : base()
        {
            PaymentID = paymentId;
            PaymentMethod = paymentMethod;
            TotalPaid = paidAmount;
        }

        protected PaymentDetailEntity() 
        { }

        public int PaymentID { get; protected set; }
        public string PaymentMethod { get; protected set; }
        public float TotalPaid { get; protected set; }
        public PaymentEntity PaymentEntity { get; set; }
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
