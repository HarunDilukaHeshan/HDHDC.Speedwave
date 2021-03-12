using AutoMapper;
using HDHDC.Speedwave.DTOs;
using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.Users;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave
{
    public class SpeedwaveApplicationAutoMapperProfile : Profile
    {
        public SpeedwaveApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */


            CreateMap<AddressEntity, AddressDto>();
            CreateMap<AddressCreateDto, AddressEntity>();
            CreateMap<AddressUpdateDto, AddressEntity>();

            CreateMap<CustomerEntity, CustomerDto>();
            CreateMap<CustomerCreateDto, CustomerEntity>();

            CreateMap<ProvinceDto, ProvinceEntity>();
            CreateMap<ProvinceEntity, ProvinceDto>();

            CreateMap<DistrictCreateDto, DistrictEntity>();
            CreateMap<DistrictDto, DistrictEntity>();
            CreateMap<DistrictEntity, DistrictDto>();

            CreateMap<ManagerEntity, ManagerDto>();
            CreateMap<ManagerCreateDto, ManagerEntity>();
            CreateMap<ManagerUpdateDto, ManagerEntity>();

            CreateMap<IdentityUser, AppUserDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<IdentityUserDto, AppUserDto>();

            CreateMap<CityCreateDto, CityEntity>();
            CreateMap<CityEntity, CityDto>();
            CreateMap<CityUpdateDto, CityEntity>();

            CreateMap<RiderCreateDto, RiderEntity>();
            CreateMap<RiderUpdateDto, RiderEntity>();
            CreateMap<RiderEntity, RiderDto>();
            

            CreateMap<CategoryCreateUpdateDto, CategoryEntity>();
            CreateMap<CategoryCreateDto, CategoryEntity>();
            CreateMap<CategoryUpdateDto, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryDto>();            

            CreateMap<DeliveryScheduleCreateUpdateDto, DeliveryScheduleEntity>();
            CreateMap<DeliveryScheduleEntity, DeliveryScheduleDto>();

            CreateMap<DistanceChargeCreateUpdateDto, DistanceChargeEntity>();
            CreateMap<DistanceChargeEntity, DistanceChargeDto>();

            CreateMap<UnitDto, UnitEntity>();
            CreateMap<UnitEntity, UnitDto>();

            CreateMap<QuantityDto, QuantityEntity>();
            CreateMap<QuantityEntity, QuantityDto>();

            CreateMap<MinRequiredTimeDto, MinRequiredTimeEntity>();
            CreateMap<MinRequiredTimeEntity, MinRequiredTimeDto>();

            CreateMap<ItemCreateDto, ItemEntity>();
            CreateMap<ItemUpdateDto, ItemEntity>();
            CreateMap<ItemEntity, ItemDto>();

            CreateMap<ItemCategoryEntity, ItemCategoryDto>();
            CreateMap<ItemCategoryDto, ItemCategoryEntity>();

            CreateMap<StoreChainCreateDto, StoreChainEntity>();
            CreateMap<StoreChainUpdateDto, StoreChainEntity>();
            CreateMap<StoreChainEntity, StoreChainDto>();

            CreateMap<StoreBranchCreateDto, StoreBranchEntity>();
            CreateMap<StoreBranchUpdateDto, StoreBranchEntity>();
            CreateMap<StoreBranchEntity, StoreBranchDto>();

            CreateMap<StoreOpenDayDto, StoreOpenDayEntity>();
            CreateMap<StoreOpenDayEntity, StoreOpenDayDto>();

            CreateMap<StoreClosingDateDto, StoreClosingDateEntity>();
            CreateMap<StoreClosingDateEntity, StoreClosingDateDto>();
            
            CreateMap<PromotionEntity, PromotionDto>();
            CreateMap<PromotionCreateDto, PromotionEntity>();
            CreateMap<PromotionUpdateDto, PromotionEntity>();

            CreateMap<DistanceChargeEntity, DistanceChargeDto>();
            CreateMap<DistanceChargeCreateUpdateDto, DistanceChargeEntity>();

            CreateMap<SubtotalPercentageCreateUpdateDto, SubtotalPercentageEntity>();
            CreateMap<SubtotalPercentageEntity, SubtotalPercentageDto>();

            CreateMap<ItemStoreBranchEntity, ItemStoreBranchDto>();

            CreateMap<OrderEntity, OrderDto>();

            CreateMap<PaymentEntity, PaymentDto>();

            CreateMap<DeliveryChargeEntity, DeliveryChargeDto>();
            CreateMap<OrderItemEntity, OrderItemDto>();

            CreateMap<CancellationReasonEntity, CancellationReasonDto>();

            CreateMap<CancelledOrderDto, CancelledOrderEntity>();
            CreateMap<CancelledOrderEntity, CancelledOrderDto>();

            // Recognize aliases
            RecognizeAlias("CityEntity", "CityDto");
            RecognizeAlias("ItemEntity", "ItemDto");
            RecognizeAlias("StoreBranchEntity", "StoreBranchDto");
            RecognizeAlias("PaymentEntity", "PaymentDto");
            RecognizeAlias("DeliveryScheduleEntity", "DeliveryScheduleDto");
            RecognizeAlias("DeliveryChargeEntity", "DeliveryChargeDto");
            RecognizeAlias("AddressEntity", "AddressDto");
            RecognizeAlias("PromotionEntity", "PromotionDto");
            RecognizeAlias("RiderEntity", "RiderDto");
            RecognizeAlias("OrderItemEntities", "OrderItemDtos");

        }
    }
}
