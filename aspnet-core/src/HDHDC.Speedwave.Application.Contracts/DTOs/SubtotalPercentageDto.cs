using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class SubtotalPercentageDto : EntityDto<int>
    {
        public uint Percentage { get; set; }
        public float From { get; set; }
    }
}
