using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CityCreateDto : EntityDto
    {
        [Required]
        [MinLength(EntityConstraintsConsts.NameMinLength)]
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
        [Required]
        [RegularExpression("[a-zA-Z0-9]+[a-zA-Z-0-9 ]*")]
        public string DistrictID { get; set; }
    }
}
