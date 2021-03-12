using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreChainCreateDto : EntityDto
    {
        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        public string StoreChainName { get; set; }
        [Required]
        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        public string StoreChainDescription { get; set; }        
        [Required]
        [Base64ImageValidator]
        public string LogoBase64 { get; set; }
    }
}
