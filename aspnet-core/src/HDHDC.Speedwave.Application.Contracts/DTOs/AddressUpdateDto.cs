using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class AddressUpdateDto : EntityDto<int>
    {
        [Required]
        [MinLength(EntityConstraintsConsts.AddressMinLength)]
        [MaxLength(EntityConstraintsConsts.AddressMaxLength)]
        public string AddressLine { get; set; }
        
        /// <summary>
        /// This property has a pre specified format
        /// [latitude:longitude]
        /// [6.456578:79.5465125]
        /// </summary>
        [Required]
        [GeolocationValidator]
        public string Geolocation { get; set; }

        [MinLength(EntityConstraintsConsts.DescriptionMinLength)]
        [MaxLength(EntityConstraintsConsts.DescriptionMaxLength)]
        public string Note { get; set; }
    }
}
