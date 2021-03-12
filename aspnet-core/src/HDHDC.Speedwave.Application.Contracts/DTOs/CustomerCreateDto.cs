using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CustomerCreateDto : EntityDto
    {
        [Required]
        public Guid UserID { get; protected set; }
        public string Status { get; protected set; } = EntityStatusConsts.Active;
    }
}
