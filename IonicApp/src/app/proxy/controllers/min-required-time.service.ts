import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { MinRequiredTimeDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class MinRequiredTimeService {
  apiName = 'Default';

  createByDto = (dto: MinRequiredTimeDto) =>
    this.restService.request<any, MinRequiredTimeDto>({
      method: 'POST',
      url: `/apiv1/MinRequiredTime`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/MinRequiredTime/${id}`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, MinRequiredTimeDto>({
      method: 'GET',
      url: `/apiv1/MinRequiredTime/${id}`,
    },
    { apiName: this.apiName });

  getList = () =>
    this.restService.request<any, MinRequiredTimeDto[]>({
      method: 'GET',
      url: `/apiv1/MinRequiredTime`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: MinRequiredTimeDto) =>
    this.restService.request<any, MinRequiredTimeDto>({
      method: 'PUT',
      url: `/apiv1/MinRequiredTime/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
