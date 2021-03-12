using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HDHDC.Speedwave.DTOs
{
    public class CartItemDto
    {
        [Range(1, int.MaxValue)]
        public int ItemID { get; set; }
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
    }
}
