using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ItemDto : EntityDto<int>
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public float ItemPrice { get; set; }
        public string ItemThumbnail { get; set; }
        public int QuantityId { get; set; }
        public int MinRequiredTimeId { get; set; }
        public string NormalizedQuantityLabel { get; set; }
        public string NormalizedMinRequiredTime { get; set; }
    }
}
