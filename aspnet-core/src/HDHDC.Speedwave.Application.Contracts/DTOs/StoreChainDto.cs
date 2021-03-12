using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreChainDto : EntityDto<int>
    {
        public string StoreChainName { get; set; }
        public string StoreChainDescription { get; set; }
        public string StoreChainLogo { get; set; }
    }
}
