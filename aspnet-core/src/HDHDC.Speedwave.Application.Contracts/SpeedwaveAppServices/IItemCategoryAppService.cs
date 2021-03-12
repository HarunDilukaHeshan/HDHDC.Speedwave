using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IItemCategoryAppService 
        : ICrudAppService<ItemCategoryDto, ItemCategoryKey>
    {
    }

    public class ItemCategoryKey
    {
        public ItemCategoryKey()
        { }
        public ItemCategoryKey(int itemID, int categoryID)
        {
            ItemID = itemID;
            CategoryID = categoryID;
        }

        public int ItemID { get; set; }
        public int CategoryID { get; set; }
    }
}
