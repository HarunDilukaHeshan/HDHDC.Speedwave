using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HDHDC.Speedwave.DTOs
{
    public class ItemStoreBranchDto : EntityDto
    {
        public int ItemID { get; set; }
        public int StoreBranchID { get; set; }
        public ItemDto ItemDto { get; set; }
        public StoreBranchDto StoreBranchDto { get; set; }
    }
}
