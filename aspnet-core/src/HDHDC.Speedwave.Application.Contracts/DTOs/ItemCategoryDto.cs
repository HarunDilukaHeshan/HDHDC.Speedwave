using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ItemCategoryDto : EntityDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int ItemID { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int CategoryID { get; set; }        
    }
}
