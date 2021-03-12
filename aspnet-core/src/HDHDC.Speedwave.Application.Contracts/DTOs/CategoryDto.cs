using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class CategoryDto : EntityDto<int>
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryThumbnail { get; set; }
    }
}
