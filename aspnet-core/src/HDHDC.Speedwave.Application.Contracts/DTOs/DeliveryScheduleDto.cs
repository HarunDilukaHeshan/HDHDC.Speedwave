using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class DeliveryScheduleDto : EntityDto<int>
    {
        public string DeliveryScheduleName { get; set; }
        public TimeSpan TimePeriod { get; set; }
        public float CostIncreasePercentage { get; set; }
    }
}
