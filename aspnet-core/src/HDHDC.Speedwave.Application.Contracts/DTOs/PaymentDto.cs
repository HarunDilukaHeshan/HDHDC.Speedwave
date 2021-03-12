using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class PaymentDto : EntityDto<int>
    {
        public int DeliveryChargeID { get; set; }
        public int OrderID { get; set; }
        public float Nettotal { get; set; }
        public float Subtotal { get; set; }
        public float TotalPaid { get; set; }
        public string PaymentStatus { get; set; }
        public DeliveryChargeDto DeliveryChargeDto { get; set; }        
    }
}
