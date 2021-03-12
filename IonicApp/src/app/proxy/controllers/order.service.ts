import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CancellationReasonDto, CancelledOrderDto, CartItemDto, DeliveryChargeDto, OrderCreateDto, OrderDto, PaymentDetailDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  apiName = 'Default';

  calculateDeliveryChargeByAddressIdAndDeliveryScheduleIdAndItemsArr = (addressId: number, deliveryScheduleId: number, itemsArr: CartItemDto[]) =>
    this.restService.request<any, DeliveryChargeDto>({
      method: 'POST',
      url: `/apiv1/Order/deliverycharge`,
      params: { addressId: addressId, deliveryScheduleId: deliveryScheduleId },
      body: itemsArr,
    },
    { apiName: this.apiName });

  createByOrderCreateDto = (orderCreateDto: OrderCreateDto) =>
    this.restService.request<any, OrderDto>({
      method: 'POST',
      url: `/apiv1/Order`,
      body: orderCreateDto,
    },
    { apiName: this.apiName });

  deselectById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Order/selected/${id}`,
      params: { id: id },
    },
    { apiName: this.apiName });

  getAllCR = () =>
    this.restService.request<any, CancellationReasonDto[]>({
      method: 'GET',
      url: `/apiv1/Order/cancellationreasons`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, OrderDto>({
      method: 'GET',
      url: `/apiv1/Order/${id}`,
    },
    { apiName: this.apiName });

  getOrdersByTypeAndCityIdAndSkipCountAndMaxResultCount = (type: string = 'history', cityId?: number, skipCount?: number, maxResultCount: number = 10) =>
    this.restService.request<any, OrderDto[]>({
      method: 'GET',
      url: `/apiv1/Order`,
      params: { type: type, cityId: cityId, skipCount: skipCount, maxResultCount: maxResultCount },
    },
    { apiName: this.apiName });

  markAsDeliveredByIdAndDtoArr = (id: number, dtoArr: PaymentDetailDto[]) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/apiv1/Order/delivered`,
      params: { id: id },
      body: dtoArr,
    },
    { apiName: this.apiName });

  removeById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Order/${id}`,
    },
    { apiName: this.apiName });

  rollbackCancellationRequestById = (id: number) =>
    this.restService.request<any, OrderDto>({
      method: 'DELETE',
      url: `/apiv1/Order/cancellationrequest/${id}`,
    },
    { apiName: this.apiName });

  selectOrderById = (id: number) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/apiv1/Order/selected`,
      params: { id: id },
    },
    { apiName: this.apiName });

  sendCancellationRequestByDto = (dto: CancelledOrderDto) =>
    this.restService.request<any, CancelledOrderDto>({
      method: 'POST',
      url: `/apiv1/Order/cancellationrequest`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
