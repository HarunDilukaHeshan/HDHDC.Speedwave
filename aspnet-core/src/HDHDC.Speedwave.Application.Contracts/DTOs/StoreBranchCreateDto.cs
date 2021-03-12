using HDHDC.Speedy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreBranchCreateDto : EntityDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int StoreChainID { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CityID { get; set; }
        [Required]
        [GeolocationValidator]
        public string Geolocation { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string ContactNo01 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string ContactNo02 { get; set; }
    }
}
