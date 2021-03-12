using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreClosingDateDto : EntityDto<int>
    {
        public int StoreBranchID { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
