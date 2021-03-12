using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class SubtotalPercentageCreateUpdateDto : EntityDto
    {
        [Required]
        [Range(0, 100)]
        public uint Percentage { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float From { get; set; }
    }
}
