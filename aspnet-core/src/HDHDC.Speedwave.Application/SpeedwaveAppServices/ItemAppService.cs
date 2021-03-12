using HDHDC.Speedwave.BlobServices.Managers;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.Repositories.Services;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class ItemAppService
        : CrudAppService<
            ItemEntity,
            ItemDto,
            int,
            PagedAndSortedResultRequestDto,
            ItemCreateDto,
            ItemUpdateDto>,
        IItemAppService
    {
        protected IItemPictureManager ItemPictureManager { get; }
        protected IRepository<StoreBranchEntity, int> StoreBranchRepository { get; }
        protected IRepository<CategoryEntity, int> CategoryRepository { get; }
        protected IRepository<ItemStoreBranchEntity> ItemStoreBranchRepository { get; }
        protected IItemCategoryRepository ItemCategoryRepository { get; }

        public ItemAppService(
            IItemRepository repository,
            IRepository<StoreBranchEntity, int> storeBranchRepository,
            IRepository<CategoryEntity, int> categoryRepository,
            IRepository<ItemStoreBranchEntity> itemStoreBranchRepository,
            IItemCategoryRepository itemCategoryRepository,
            IItemPictureManager itemPictureManager)
            : base(repository)
        {
            ItemPictureManager = itemPictureManager;
            StoreBranchRepository = storeBranchRepository;
            ItemStoreBranchRepository = itemStoreBranchRepository;
            CategoryRepository = categoryRepository;
            ItemCategoryRepository = itemCategoryRepository;
        }

        public override async Task<ItemDto> CreateAsync(ItemCreateDto input)
        {
            var itemE = ObjectMapper.Map<ItemCreateDto, ItemEntity>(input);
            itemE.ItemThumbnail = "";

            await Repository.InsertAsync(itemE, autoSave: true);

            itemE.ItemThumbnail = await ItemPictureManager.SaveAsync(itemE.Id, input.ThumbnailBase64);

            await Repository.UpdateAsync(itemE);

            return ObjectMapper.Map<ItemEntity, ItemDto>(itemE);
        }

        public override async Task<ItemDto> UpdateAsync(int id, ItemUpdateDto input)
        {            
            return await base.UpdateAsync(id, input);
        }

        public async Task<string> UpdateThumbnail(int id, string base64Thumbnail)
        {
            var itemE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            var fileName = await ItemPictureManager.SaveThumbnailAsync(base64Thumbnail);

            // await ItemPictureManager.DeleteAsync(itemE.ItemThumbnail);

            itemE.ItemThumbnail = fileName;

            await Repository.UpdateAsync(itemE);

            return fileName;
        }

        public async Task<string> AddPicture(int id, string base64Image)
        {
            _= await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            var fileName = await ItemPictureManager.SaveAsync(id, base64Image, true);

            return fileName;
        }

        public async Task RemovePicture(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException();

            await ItemPictureManager.DeleteAsync(fileName);
        }

        public async Task AddToStoreBranch(int itemId, params int[] storeBranchIds)
        {
            _ = await Repository.FindAsync(itemId) ?? throw new EntityNotFoundException();

            foreach(var id in storeBranchIds)
            {
                var storeBranchE = await StoreBranchRepository.GetAsync(id) 
                    ?? throw new EntityNotFoundException("One of the specified store branches does not exist");
                await ItemStoreBranchRepository.InsertAsync(new ItemStoreBranchEntity(itemId, storeBranchE.Id));
            }
        }

        public async Task AddToCategory(int itemId, int[] categoryIds)
        {
            _ = await Repository.FindAsync(itemId) ?? throw new EntityNotFoundException();

            foreach (var id in categoryIds)
            {
                var categoryE = await CategoryRepository.GetAsync(id)
                    ?? throw new EntityNotFoundException("One of the specified categories does not exist");
                await ItemCategoryRepository.InsertAsync(new ItemCategoryEntity(itemId, categoryE.Id));
            }
        }

        public async Task UpdateItemCategory(int id, int[] categoryIds)
        {
            foreach(var catId in categoryIds)
                _= await CategoryRepository.GetAsync(catId) ?? throw new EntityNotFoundException("One of the specified categories does not exist");

            var itemCatList = await ItemCategoryRepository.GetListAsync(e => e.ItemID == id);
            var idsList = categoryIds.Distinct().ToList();

            foreach (var itemCat in itemCatList.ToArray())            
                if (idsList.Contains(itemCat.CategoryID)) 
                { 
                    idsList.Remove(itemCat.CategoryID);
                    itemCatList.Remove(itemCat);
                }

            foreach (var itemCat in itemCatList)
                await ItemCategoryRepository.DeleteAsync(e => e.ItemID == id && e.CategoryID == itemCat.CategoryID);

            foreach (var cat in idsList)
                await ItemCategoryRepository.InsertAsync(new ItemCategoryEntity(id, cat));            
        }

        public async Task<ItemCategoryDto[]> GetAllItemCategory(int id)
        {
            var itemCatEsList = await ItemCategoryRepository.GetListAsync(e => e.ItemID == id);

            return ObjectMapper.Map<ItemCategoryEntity[], ItemCategoryDto[]>(itemCatEsList.ToArray());
        }

        public async Task<ItemDto[]> SearchAsync(string keywords = "", int skipCount = 0, int maxResultCount = 10)
        {
            var dtoList = await ((IItemRepository)Repository).SearchAsync(keywords, skipCount, maxResultCount);            

            return ObjectMapper.Map<ItemEntity[], ItemDto[]>(dtoList);
        }

        public async Task<ThumbnailDto> GetThumbnail(string thumbnailName)
        {
            var dto = new ThumbnailDto
            {
                 Base64DataUrl = await ItemPictureManager.GetAsync(thumbnailName)
            };

            return dto;
        }

        public async Task<BlobFileDto[]> GetAllItemPic(int id)
        {
            _= (await Repository.FindAsync(id)) ?? throw new EntityNotFoundException();
            var blobFileList = await ItemPictureManager.GetBlobFileListAsync(id);
            var dtoList = new List<BlobFileDto>();

            foreach (var bf in blobFileList) {
                var b64Str = string.Concat("data:image/jpg;base64,", Convert.ToBase64String(bf.Blob));
                dtoList.Add(new BlobFileDto(bf.FileName, b64Str));
            }

            return dtoList.ToArray();
        }

        public async Task<BlobFileDto> AddItemPic(int id, BlobFileDto fileDto)
        {
            _ = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            var fileName = await ItemPictureManager.SaveAsync(id, fileDto.Base64DataUri, true);

            return new BlobFileDto(fileName, "");
        }

        public async Task RemoveItemPic(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException();

            await ItemPictureManager.DeleteAsync(fileName);
        }        

        public async Task<ItemDto[]> SelectAsync(int seed, int maxResultCount = 10)
        {
            var entityArr = await ((IItemRepository)Repository).SelectItemsAsync(seed, maxResultCount);
            return ObjectMapper.Map<ItemEntity[], ItemDto[]>(entityArr);
        }

        public async Task<ItemDto[]> GetItemsWithDetailAsync(int[] itemIdsArr)
        {
            _ = itemIdsArr ?? throw new BusinessException();

            var itemEList = new List<ItemEntity>();

            foreach(int id in itemIdsArr)
            {
                var itemE = await Repository.GetAsync(e => e.IsDeleted == false && e.Id == id);
                itemEList.Add(itemE);
            }            

            return ObjectMapper.Map<ItemEntity[], ItemDto[]>(itemEList.ToArray());
        }

        public async Task<ItemDto[]> SearchWithinTheRadiusAsync(
            int cityId, 
            string keywords = "", 
            int skipCount = 0, 
            int maxResultCount = 10)
        {
            var itemEArr = await ((IItemRepository)Repository).SearchWithinTheRadiusAsync(cityId, keywords, skipCount, maxResultCount);
            return ObjectMapper.Map<ItemEntity[], ItemDto[]>(itemEArr);
        }

        public async Task<ItemDto[]> RndSelectWithinTheRadiusAsync(
            int cityId, 
            int seed = 0, 
            int maxResultCount = 10)
        {
            var itemEArr = await ((IItemRepository)Repository).RndSelectWithinTheRadiusAsync(cityId, seed, maxResultCount);
            return ObjectMapper.Map<ItemEntity[], ItemDto[]>(itemEArr);
        }
    }
}
