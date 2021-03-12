using HDHDC.Speedwave.BlobServices.Containers;
using HDHDC.Speedwave.BlobServices.Managers;
using HDHDC.Speedwave.BlobStoringServices;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.SpeedwaveAppServices
{
    public class StoreChainAppService 
        : CrudAppService<
            StoreChainEntity, 
            StoreChainDto,
            int,
            PagedAndSortedResultRequestDto,
            StoreChainCreateDto,
            StoreChainUpdateDto>,
        IStoreChainAppService
    {
        protected IStoreChainLogoManager StoreChainLogoManager { get; }
        protected IRepository<StoreBranchEntity, int> StoreBranchRepository { get; }
        public StoreChainAppService(
            IRepository<StoreChainEntity, int> repository,
            IRepository<StoreBranchEntity, int> storeBranchRepository,
            IStoreChainLogoManager storeChainLogoManager)
            : base(repository)
        {
            StoreChainLogoManager = storeChainLogoManager;
            StoreBranchRepository = storeBranchRepository;
        }

        public override async Task<StoreChainDto> CreateAsync(StoreChainCreateDto input)
        {
            var storeChainE = ObjectMapper.Map<StoreChainCreateDto, StoreChainEntity>(input);

            storeChainE.StoreChainLogo = await StoreChainLogoManager.SaveAsync(input.LogoBase64);

            await Repository.InsertAsync(storeChainE, true);

            return ObjectMapper.Map<StoreChainEntity, StoreChainDto>(storeChainE);
        }

        public override async Task<StoreChainDto> UpdateAsync(int id, StoreChainUpdateDto input)
        {
            var storeChainE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            ObjectMapper.Map(input, storeChainE);             

            await Repository.UpdateAsync(storeChainE);

            return ObjectMapper.Map<StoreChainEntity, StoreChainDto>(storeChainE);
        }

        public async Task<StoreChainDto> UpdateLogoAsync(int id, ThumbnailDto dto)
        {
            var storeChainE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            var newLogoFileName = await StoreChainLogoManager.SaveAsync(dto.Base64DataUrl);
            await StoreChainLogoManager.DeleteAsync(storeChainE.StoreChainLogo);
            storeChainE.StoreChainLogo = newLogoFileName;
            await Repository.UpdateAsync(storeChainE);

            return ObjectMapper.Map<StoreChainEntity, StoreChainDto>(storeChainE);
        }

        public async Task<ThumbnailDto> GetLogoAsync(int id)
        {
            var storeChainE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();
            var logo = await StoreChainLogoManager.GetAsync(storeChainE.StoreChainLogo);

            return new ThumbnailDto { Base64DataUrl = logo };
        }

        public override async Task DeleteAsync(int id)
        {
            var storeChainE = await Repository.GetAsync(id) ?? throw new EntityNotFoundException();

            var storeBranchE = await StoreBranchRepository.FindAsync(e => e.StoreChainID == id);

            if (storeBranchE != null) throw new InvalidOperationException("");

            await base.DeleteAsync(id);
        }
    }
}
