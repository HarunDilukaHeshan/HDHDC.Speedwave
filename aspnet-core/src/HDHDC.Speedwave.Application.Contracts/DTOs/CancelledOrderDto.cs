using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CancelledOrderDto : EntityDto<int>
    {
        public int CancellationReasonId { get; set; }
        public string Description { get; set; }
        public CancellationReasonDto CancellationReasonDto { get; set; }
        public OrderDto OrderDto { get; set; }
    }
}
