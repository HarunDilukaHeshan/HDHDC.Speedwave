using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class StoreBranchDto : EntityDto<int>
    {
        public int StoreChainID { get; set; }
        public int CityID { get; protected set; }
        public CityDto CityDto { get; set; }
        public string Geolocation { get; set; }
        public string ContactNo01 { get; set; }
        public string ContactNo02 { get; set; }
    }
}
