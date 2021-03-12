using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ManagerCreateDto : EntityDto
    {                
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public UserCreateDto UserCreateDto { get; set; }
    }
}
