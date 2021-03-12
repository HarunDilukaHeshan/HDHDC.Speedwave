using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ManagerUpdateDto : EntityDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public string DistrictID { get; set; }        
    }
}
