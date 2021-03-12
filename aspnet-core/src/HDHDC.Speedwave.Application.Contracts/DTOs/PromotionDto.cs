using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class PromotionDto : EntityDto<int>
    {
        public bool IsOneTime { get; set; }
        public uint NoOfTimes { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
