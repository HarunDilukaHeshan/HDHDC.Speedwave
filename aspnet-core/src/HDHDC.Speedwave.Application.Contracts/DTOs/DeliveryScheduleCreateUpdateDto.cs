using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class DeliveryScheduleCreateUpdateDto : EntityDto
    {
        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        public string DeliveryScheduleName { get; set; }
        [Required]
        public TimeSpan TimePeriod { get; set; }
        [Range(0, float.MaxValue)]
        public float CostIncreasePercentage { get; set; }
    }
}
