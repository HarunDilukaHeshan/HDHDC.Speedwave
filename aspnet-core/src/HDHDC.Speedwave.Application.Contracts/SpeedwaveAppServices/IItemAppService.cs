using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public interface IItemAppService 
        : ICrudAppService<
            ItemDto, 
            int,
            PagedAndSortedResultRequestDto,
            ItemCreateDto, 
            ItemUpdateDto>
    {

        Task<string> UpdateThumbnail(int id, string base64Thumbnail);
        Task<string> AddPicture(int id, string base64Image);
        Task RemovePicture(string fileName);
        Task AddToStoreBranch(int itemId, params int[] storeBranchIds);
        Task AddToCategory(int itemId, params int[] categoryIds);
        Task<ItemDto[]> SearchAsync(string keywords = "", int skipCount = 0, int maxResultCount = 10);
        Task<ThumbnailDto> GetThumbnail(string thumbnailName);        
        Task<ItemCategoryDto[]> GetAllItemCategory(int id);
        Task UpdateItemCategory(int id, int[] categoryIds);
        Task<BlobFileDto[]> GetAllItemPic(int id);
        Task<BlobFileDto> AddItemPic(int id, BlobFileDto fileDto);
        Task RemoveItemPic(string fileName);
        Task<ItemDto[]> SelectAsync(int seed, int maxResultCount = 10);
        Task<ItemDto[]> GetItemsWithDetailAsync(int[] itemIdsArr);
        Task<ItemDto[]> SearchWithinTheRadiusAsync(int cityId, string keywords = "", int skipCount = 0, int maxResultCount = 10);
        Task<ItemDto[]> RndSelectWithinTheRadiusAsync(int cityId, int seed = 0, int maxResultCount = 10);
    }
}
