using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class PasswordResetDto : EntityDto
    {
        [Required]
        public string UserName { get; set; }
        public string PasswordResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
