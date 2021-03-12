﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class DistanceChargeDto : EntityDto<int>
    {
        public float Charge { get; set; }
        public uint From { get; set; }
    }
}
