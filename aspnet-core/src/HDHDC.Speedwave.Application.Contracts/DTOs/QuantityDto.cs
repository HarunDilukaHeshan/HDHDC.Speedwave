using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class QuantityDto : EntityDto<int>
    {
        [Required]
        [Range(0.001, float.MaxValue)]
        public float Quantity { get; set; }
        [Required]
        public string UnitID { get; set; }
        public string NormalizedQuantityLabel { get; set; }
    }
}
