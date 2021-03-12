import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { QuantityDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class QuantityService {
  apiName = 'Default';

  createByDto = (dto: QuantityDto) =>
    this.restService.request<any, QuantityDto>({
      method: 'POST',
      url: `/apiv1/Quantity`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Quantity/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, QuantityDto[]>({
      method: 'GET',
      url: `/apiv1/Quantity`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, QuantityDto>({
      method: 'GET',
      url: `/apiv1/Quantity/${id}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: QuantityDto) =>
    this.restService.request<any, QuantityDto>({
      method: 'PUT',
      url: `/apiv1/Quantity/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
