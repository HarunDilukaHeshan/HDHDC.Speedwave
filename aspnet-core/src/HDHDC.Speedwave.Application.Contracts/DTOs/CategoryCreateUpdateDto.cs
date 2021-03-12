using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CategoryCreateUpdateDto : EntityDto
    {
        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9\-\(\) ]*")]
        public string CategoryName { get; set; }
        [Required]
        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9\-\(\) ]*")]
        public string CategoryDescription { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9]*.[a-zA-Z0-9]+")]
        public string CategoryThumbnail { get; set; }
                
        public string ThumbnailBase64 { get; set; }
    }
}
