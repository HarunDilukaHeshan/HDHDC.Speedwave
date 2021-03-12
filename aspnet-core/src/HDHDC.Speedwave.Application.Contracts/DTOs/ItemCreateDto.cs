using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ItemCreateDto : EntityDto
    {
        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        public string ItemName { get; set; }
        [Required]
        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        public string ItemDescription { get; set; }
        [Required]
        [Range(0.01, float.MaxValue)]
        public float ItemPrice { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int MinRequiredTimeId { get; set; }
        [Required]
        [Base64ImageValidator]
        public string ThumbnailBase64 { get; set; }
    }
}
