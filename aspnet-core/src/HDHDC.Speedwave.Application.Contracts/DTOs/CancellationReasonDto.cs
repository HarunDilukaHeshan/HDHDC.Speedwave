using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CancellationReasonDto : EntityDto<int>
    {
        public string CancellationReason { get; set; }
        public string Description { get; set; }
    }
}
