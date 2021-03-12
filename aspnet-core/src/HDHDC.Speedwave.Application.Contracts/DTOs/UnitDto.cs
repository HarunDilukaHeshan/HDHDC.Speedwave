using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class UnitDto : EntityDto<string>
    {
        [Required]
        [MinLength(1)]
        [MaxLength(EntityConstraintsConsts.UnitSymbolMaxLength)]
        public string UnitSymbol { get; set; }
    }
}
