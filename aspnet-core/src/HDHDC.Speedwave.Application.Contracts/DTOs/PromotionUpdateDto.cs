using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class PromotionUpdateDto : EntityDto<int>
    {
        [Range(1, int.MaxValue)]
        public int NoOfTimes { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
