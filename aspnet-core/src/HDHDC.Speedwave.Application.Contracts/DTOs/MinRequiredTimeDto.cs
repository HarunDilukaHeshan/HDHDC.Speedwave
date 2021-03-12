using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class MinRequiredTimeDto : EntityDto<int>
    {
        [Required]
        public TimeSpan MinRequiredTime { get; set; }
    }
}
