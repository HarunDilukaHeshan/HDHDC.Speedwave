﻿using HDHDC.Speedwave.SpeedyConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace HDHDC.Speedwave.DTOs
{
    public class UserCreateDto : EntityDto
    {
        public virtual Guid? TenantId { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9]*")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        public string EmailAddress { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        [DataType(DataType.PhoneNumber)]
        public virtual string PhoneNumber { get; set; }

        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9]*")]
        public virtual string Name { get; set; }

        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9]*")]
        public virtual string Surname { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }
    }
}
