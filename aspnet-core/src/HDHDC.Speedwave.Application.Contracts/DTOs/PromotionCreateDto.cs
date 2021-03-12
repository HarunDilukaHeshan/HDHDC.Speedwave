using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    [PromotionCrossValidator]
    public class PromotionCreateDto : EntityDto
    {
        public bool IsOneTime { get; set; }
        public int NoOfTimes { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
