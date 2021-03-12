import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DistanceChargeCreateUpdateDto, DistanceChargeDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class DistanceChargeService {
  apiName = 'Default';

  createByDto = (dto: DistanceChargeCreateUpdateDto) =>
    this.restService.request<any, DistanceChargeDto>({
      method: 'POST',
      url: `/apiv1/DistanceCharge`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/DistanceCharge/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, DistanceChargeDto[]>({
      method: 'GET',
      url: `/apiv1/DistanceCharge`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, DistanceChargeDto>({
      method: 'GET',
      url: `/apiv1/DistanceCharge/${id}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: DistanceChargeCreateUpdateDto) =>
    this.restService.request<any, DistanceChargeDto>({
      method: 'PUT',
      url: `/apiv1/DistanceCharge/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
