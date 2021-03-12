import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ProvinceDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ProvinceService {
  apiName = 'Default';

  createByDto = (dto: ProvinceDto) =>
    this.restService.request<any, ProvinceDto>({
      method: 'POST',
      url: `/apiv1/Province`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Province/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, ProvinceDto[]>({
      method: 'GET',
      url: `/apiv1/Province`,
    },
    { apiName: this.apiName });

  getById = (id: string) =>
    this.restService.request<any, ProvinceDto>({
      method: 'GET',
      url: `/apiv1/Province/${id}`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
