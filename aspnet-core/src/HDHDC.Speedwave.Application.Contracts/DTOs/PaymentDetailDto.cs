using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class PaymentDetailDto : EntityDto<int>
    {
        public PaymentDetailDto(int id, int paymentId, string paymentMethod, float totalPaid)
        {
            Id = id;
            PaymentID = paymentId;
            PaymentMethod = paymentMethod;
            TotalPaid = totalPaid;
        }

        public int PaymentID { get; protected set; }
        public string PaymentMethod { get; protected set; }
        public float TotalPaid { get; protected set; }
    }
}
