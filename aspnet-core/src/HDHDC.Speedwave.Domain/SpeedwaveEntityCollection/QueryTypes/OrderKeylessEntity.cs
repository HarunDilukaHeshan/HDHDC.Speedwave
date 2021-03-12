using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection.QueryTypes
{
    [NotMapped]
    public class OrderKeylessEntity
    {
        public int AddressId { get; set; }
        public int OrderId { get; set; }
        public int DeliveryScheduleId { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryChargeId { get; set; }
        public int? PromotionId { get; set; }        
        public float Nettotal { get; set; }
        public float Subtotal { get; set; }
        public float SubtotalPercentage { get; set; }
        public float DistanceCharge { get; set; }
        public float DeliveryScheduleCost { get; set; }
        public float DeliveryCharge { get; set; }
    }
}
