using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.DTOs
{
    public class AddressDto : EntityDto<int>
    {
        public string AddressLine { get; set; }
        public int CityID { get; set; }
        public int CustomerID { get; set; }
        /// <summary>
        /// This property has a pre specified format
        /// [latitude:longitude]
        /// [6.456578:79.5465125]
        /// </summary>
        public string Geolocation { get; set; }
        public string Note { get; set; }

        public CityDto CityDto { get; set; }
    }
}
