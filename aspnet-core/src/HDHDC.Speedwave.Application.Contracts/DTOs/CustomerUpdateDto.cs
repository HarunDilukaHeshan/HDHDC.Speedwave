using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CustomerUpdateDto : EntityDto<int>
    {
        [Required]
        public string Status { get; set; }
    }
}
