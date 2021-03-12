using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class OrderCreateDto : EntityDto
    {
        [Range(1, int.MaxValue)]
        public int AddressID { get; set; }
        [Range(1, int.MaxValue)]
        public int DeliveryScheduleID { get; set; }
        [Range(1, int.MaxValue)]
        public int? PromotionID { get; set; }
        [Required]
        [MinLength(1)]
        public List<CartItemDto> Items { get; set; }
    }
}
