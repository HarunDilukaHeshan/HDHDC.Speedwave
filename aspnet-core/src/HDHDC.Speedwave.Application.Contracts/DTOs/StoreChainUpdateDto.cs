using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreChainUpdateDto
    {
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        public string StoreChainName { get; set; }

        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        public string StoreChainDescription { get; set; }

        [Base64ImageValidator]
        public string LogoBase64 { get; set; }
    }
}
