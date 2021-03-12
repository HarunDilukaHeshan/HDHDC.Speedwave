using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class DeliveryChargeDto : EntityDto<int>
    {
        public int DistanceChargeID { get; set; }
        public int SubtotalPercentageID { get; set; }
        public int DeliveryScheduleID { get; set; }
        public float DistanceCharge { get; set; }
        public float SubtotalPercentage { get; set; }
        public float IncreasedCost { get; set; }
        public float Subtotal { get; set; }
        public float Nettotal { get; set; }
        public float Charge { get; set; }
    }
}
