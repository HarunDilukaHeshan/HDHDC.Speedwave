using HDHDC.Speedwave.SpeedyConsts;
using HDHDC.Speedy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class RiderCreateDto : EntityDto
    {
        [Required]
        public Guid UserID { get; protected set; }
        /// <summary>
        /// This property has a pre specified format
        /// [latitude:longitude]
        /// [6.456578:79.5465125]
        /// </summary>
        [Required]
        [GeolocationValidator]
        public string Geolocation { get; set; }
        [Required]
        public int CityID { get; set; }
        public UserCreateDto UserCreateDto { get; set; }
        public string Status { get; protected set; } = EntityStatusConsts.Active;
    }
}
