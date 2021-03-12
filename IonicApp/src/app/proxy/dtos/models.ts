import type { EntityDto } from '@abp/ng.core';
import { DayOfTheWeek } from 'src/app/shared/DayOfTheWeek';

export interface AddressCreateDto extends EntityDto {
  addressLine: string;
  cityID: number;
  customerID: number;
  geolocation: string;
  note: string;
}

export interface AddressDto extends EntityDto<number> {
  addressLine: string;
  cityID: number;
  customerID: number;
  geolocation: string;
  note: string;
  cityDto: CityDto;
}

export interface AddressUpdateDto extends EntityDto<number> {
  addressLine: string;
  geolocation: string;
  note: string;
}

export interface AppUserDto extends EntityDto<string> {
  tenantId?: string;
  userName: string;
  name: string;
  surname: string;
  email: string;
  emailConfirmed: boolean;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
}

export interface BlobFileDto {
  fileName: string;
  base64DataUri: string;
}

export interface CancellationReasonDto extends EntityDto<number> {
  cancellationReason: string;
  description: string;
}

export interface CancelledOrderDto extends EntityDto<number> {
  cancellationReasonId: number;
  description: string;
  cancellationReasonDto: CancellationReasonDto;
  orderDto: OrderDto;
}

export interface CartItemDto {
  itemID: number;
  qty: number;
}

export interface CategoryCreateDto extends EntityDto {
  categoryName: string;
  categoryDescription: string;
  thumbnailBase64: string;
}

export interface CategoryDto extends EntityDto<number> {
  categoryName: string;
  categoryDescription: string;
  categoryThumbnail: string;
  categoryThumbnailBase64: string;
}

export interface CategoryUpdateDto extends EntityDto<number> {
  categoryName: string;
  categoryDescription: string;
  thumbnailBase64: string;
}

export interface CityCreateDto extends EntityDto {
  cityName: string;
  geolocation: string;
  districtID: string;
}

export interface CityDto extends EntityDto<number> {
  cityName: string;
  geolocation: string;
  districtID: string;
  creatorId: string;
  creationTime: string;
  lastModifierId?: string;
  lastModificationTime?: string;
  deleterId?: string;
  deletionTime?: string;
  isDeleted: boolean;
  concurrencyStamp: string;
}

export interface CityUpdateDto extends EntityDto<number> {
  cityName: string;
  geolocation: string;
}

export interface DeliveryChargeDto extends EntityDto<number> {
  distanceChargeID: number;
  subtotalPercentageID: number;
  deliveryScheduleID: number;
  distanceCharge: number;
  subtotalPercentage: number;
  increasedCost: number;
  subtotal: number;
  nettotal: number;
  charge: number;
}

export interface DeliveryScheduleCreateUpdateDto extends EntityDto {
  deliveryScheduleName: string;
  timePeriod: string;
  costIncreasePercentage: number;
}

export interface DeliveryScheduleDto extends EntityDto<number> {
  deliveryScheduleName: string;
  timePeriod: string;
  costIncreasePercentage: number;
}

export interface DistanceChargeCreateUpdateDto extends EntityDto {
  charge: number;
  from: number;
}

export interface DistanceChargeDto extends EntityDto<number> {
  charge: number;
  from: number;
}

export interface DistrictCreateDto extends EntityDto<string> {
  id: string;
  provinceID: string;
}

export interface DistrictDto extends EntityDto<string> {
  provinceID: string;
  creatorId: string;
  creationTime: string;
  deleterId?: string;
  deletionTime?: string;
  isDeleted: boolean;
  concurrencyStamp: string;
}

export interface ItemCategoryDto extends EntityDto {
  itemID: number;
  categoryID: number;
}

export interface ItemCreateDto extends EntityDto {
  itemName: string;
  itemDescription: string;
  itemPrice: number;
  quantityId: number;
  minRequiredTimeId: number;
  thumbnailBase64: string;
}

export interface ItemDto extends EntityDto<number> {
  itemName: string;
  itemDescription: string;
  itemPrice: number;
  itemThumbnail: string;
  quantityId: number;
  minRequiredTimeId: number;
  normalizedQuantityLabel: string;
  normalizedMinRequiredTime: string;
}

export interface ItemStoreBranchDto extends EntityDto {
  itemID: number;
  storeBranchID: number;
  itemDto: ItemDto;
  storeBranchDto: StoreBranchDto;
}

export interface ItemUpdateDto extends EntityDto {
  itemName: string;
  itemDescription: string;
  itemPrice: number;
  quantityId: number;
  minRequiredTimeId: number;
  itemThumbnail: string;
}

export interface ManagerCreateDto extends EntityDto {
  districtID: string;
  userCreateDto: UserCreateDto;
}

export interface ManagerDto extends EntityDto<number> {
  userID: string;
  districtID: string;
  status: string;
  appUser: AppUserDto;
  creatorId: string;
  creationTime: string;
  lastModifierId?: string;
  lastModificationTime?: string;
  deleterId?: string;
  deletionTime?: string;
  isDeleted: boolean;
  concurrencyStamp: string;
}

export interface ManagerUpdateDto extends EntityDto {
  districtID: string;
}

export interface MinRequiredTimeDto extends EntityDto<number> {
  minRequiredTime: string;
}

export interface OrderCreateDto extends EntityDto {
  addressID: number;
  deliveryScheduleID: number;
  promotionID?: number;
  items: CartItemDto[];
}

export interface OrderDto extends EntityDto<number> {
  paymentID: number;
  deliveryScheduleID: number;
  addressID: number;
  promotionID?: number;
  orderStatus: string;
  paymentDto: PaymentDto;
  deliveryScheduleDto: DeliveryScheduleDto;
  addressDto: AddressDto;
  promotionDto: PromotionDto;
  riderDto: RiderDto;
  orderItemDtos: OrderItemDto[];
}

export interface OrderItemDto extends EntityDto {
  orderID: number;
  itemID: number;
  itemPrice: number;
  quantity: number;
  itemDto: ItemDto;
}

export interface PasswordResetDto extends EntityDto {
  userName: string;
  passwordResetToken: string;
  newPassword: string;
}

export interface PaymentDetailDto extends EntityDto<number> {
  paymentID: number;
  paymentMethod: string;
  totalPaid: number;
}

export interface PaymentDto extends EntityDto<number> {
  deliveryChargeID: number;
  orderID: number;
  nettotal: number;
  subtotal: number;
  totalPaid: number;
  paymentStatus: string;
  deliveryChargeDto: DeliveryChargeDto;
}

export interface PromotionCreateDto extends EntityDto {
  isOneTime: boolean;
  noOfTimes: number;
  expireDate: string;
}

export interface PromotionDto extends EntityDto<number> {
  isOneTime: boolean;
  noOfTimes: number;
  expireDate: string;
}

export interface PromotionUpdateDto extends EntityDto<number> {
  noOfTimes: number;
  expireDate: string;
}

export interface ProvinceDto extends EntityDto<string> {
  id: string;
}

export interface QuantityDto extends EntityDto<number> {
  quantity: number;
  unitID: string;
  normalizedQuantityLabel: string;
}

export interface RiderCreateDto extends EntityDto {
  userID: string;
  geolocation: string;
  cityID: number;
  userCreateDto: UserCreateDto;
  status: string;
}

export interface RiderDto extends EntityDto<number> {
  userID: string;
  geolocation: string;
  cityID: number;
  status: string;
  appUser: AppUserDto;
  cityDto: CityDto;
  creationTime: string;
  lastModifierId?: string;
  lastModificationTime?: string;
  deleterId?: string;
  deletionTime?: string;
  isDeleted: boolean;
  concurrencyStamp: string;
}

export interface RiderUpdateDto extends EntityDto {
  geolocation: string;
  cityID: number;
  status: string;
}

export interface SlideShowPictureDto {
  name: string;
  base64Picture: string;
  uri: string;
  fileName: string;
}

export interface StoreBranchCreateDto extends EntityDto {
  storeChainID: number;
  cityID: number;
  geolocation: string;
  contactNo01: string;
  contactNo02: string;
}

export interface StoreBranchDto extends EntityDto<number> {
  storeChainID: number;
  cityID: number;
  cityDto: CityDto;
  geolocation: string;
  contactNo01: string;
  contactNo02: string;
}

export interface StoreBranchUpdateDto extends EntityDto {
  geolocation: string;
  contactNo01: string;
  contactNo02: string;
}

export interface StoreChainCreateDto extends EntityDto {
  storeChainName: string;
  storeChainDescription: string;
  logoBase64: string;
}

export interface StoreChainDto extends EntityDto<number> {
  storeChainName: string;
  storeChainDescription: string;
  storeChainLogo: string;
}

export interface StoreChainUpdateDto {
  storeChainName: string;
  storeChainDescription: string;
  logoBase64: string;
}

export interface StoreClosingDateDto extends EntityDto<number> {
  storeBranchID: number;
  closingDate: string;
}

export interface StoreOpenDayDto extends EntityDto {
  storeBranchID: number;
  dayOfWeek: DayOfTheWeek;
  openingTime: string;
  closingTime: string;
}

export interface SubtotalPercentageCreateUpdateDto extends EntityDto {
  percentage: number;
  from: number;
}

export interface SubtotalPercentageDto extends EntityDto<number> {
  percentage: number;
  from: number;
}

export interface ThumbnailDto {
  base64DataUrl: string;
}

export interface UnitDto extends EntityDto<string> {
  unitSymbol: string;
}

export interface UserCreateDto extends EntityDto {
  tenantId?: string;
  userName: string;
  emailAddress: string;
  password: string;
  phoneNumber: string;
  name: string;
  surname: string;
  emailConfirmed: boolean;
  phoneNumberConfirmed: boolean;
}
