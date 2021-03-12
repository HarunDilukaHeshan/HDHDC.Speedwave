import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DeliveryScheduleCreateUpdateDto, DeliveryScheduleDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class DeliveryScheduleService {
  apiName = 'Default';

  createByDto = (dto: DeliveryScheduleCreateUpdateDto) =>
    this.restService.request<any, DeliveryScheduleDto>({
      method: 'POST',
      url: `/apiv1/DeliverySchedule`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/DeliverySchedule/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, DeliveryScheduleDto[]>({
      method: 'GET',
      url: `/apiv1/DeliverySchedule`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, DeliveryScheduleDto>({
      method: 'GET',
      url: `/apiv1/DeliverySchedule/${id}`,
    },
    { apiName: this.apiName });

  getCompatiblesByAddressIdAndItemIds = (addressId: number, itemIds: number[]) =>
    this.restService.request<any, DeliveryScheduleDto[]>({
      method: 'GET',
      url: `/apiv1/DeliverySchedule/compatibles`,
      params: { addressId: addressId, itemIds: itemIds },
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: DeliveryScheduleCreateUpdateDto) =>
    this.restService.request<any, DeliveryScheduleDto>({
      method: 'PUT',
      url: `/apiv1/DeliverySchedule/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
