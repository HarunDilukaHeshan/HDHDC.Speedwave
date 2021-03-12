﻿using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CategoryUpdateDto : EntityDto<int>
    {
        [MinLength(EntityConstraintsConsts.NameMinLength)]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9\-\(\) ]*")]
        public string CategoryName { get; set; }

        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9\-\(\) ]*")]
        public string CategoryDescription { get; set; }

        [Base64ImageValidator]
        public string ThumbnailBase64 { get; set; }
    }
}
