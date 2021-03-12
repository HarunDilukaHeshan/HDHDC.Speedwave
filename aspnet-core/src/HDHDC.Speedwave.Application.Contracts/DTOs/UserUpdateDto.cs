using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace HDHDC.Speedwave.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        [EmailAddress]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        public string EmailAddress { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        [DataType(DataType.PhoneNumber)]
        public virtual string PhoneNumber { get; set; }

        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression("[a-zA-Z0-9]+[a-zA-Z0-9]*")]
        public virtual string Name { get; set; }

        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression("[a-zA-Z0-9]+[a-zA-Z0-9]*")]
        public virtual string Surname { get; set; }
    }
}
