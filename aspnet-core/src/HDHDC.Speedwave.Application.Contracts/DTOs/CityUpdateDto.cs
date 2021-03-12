using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CityUpdateDto : EntityDto<int>
    {
        [Required]
        [MaxLength(EntityConstraintsConsts.NameMaxLength)]
        public string CityName { get; set; }
        /// <summary>
        /// This property has a pre specified format
        /// [latitude:longitude]
        /// [6.456578:79.5465125]
        /// </summary>
        [Required]
        [GeolocationValidator]
        public string Geolocation { get; set; }
    }
}
