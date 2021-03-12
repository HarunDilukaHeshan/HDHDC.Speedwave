using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class OrderItemDto : EntityDto
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public float ItemPrice { get; set; }
        public uint Quantity { get; set; }
        public ItemDto ItemDto { get; set; }
        //public OrderDto OrderDto { get; set; }
    }
}
