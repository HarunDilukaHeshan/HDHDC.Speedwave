using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreOpenDayUpdateDto : EntityDto
    {
        [Required]
        public TimeSpan OpeningTime { get; set; }
        [Required]
        public TimeSpan ClosingTime { get; set; }
    }
}
