using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class OrderDto : EntityDto<int>
    {
        public int PaymentID { get; set; }
        public int DeliveryScheduleID { get; set; }
        public int AddressID { get; set; }
        public int? PromotionID { get; set; }
        public string OrderStatus { get; set; }
        public PaymentDto PaymentDto { get; set; }
        public DeliveryScheduleDto DeliveryScheduleDto { get; set; }
        public AddressDto AddressDto { get; set; }
        public PromotionDto PromotionDto { get; set; }
        public RiderDto RiderDto { get; set; }
        public IList<OrderItemDto> OrderItemDtos { get; set; }
    }
}
