import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DistrictCreateDto, DistrictDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class DistrictService {
  apiName = 'Default';

  createByDto = (dto: DistrictCreateDto) =>
    this.restService.request<any, DistrictDto>({
      method: 'POST',
      url: `/apiv1/District`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/District/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, DistrictDto[]>({
      method: 'GET',
      url: `/apiv1/District`,
    },
    { apiName: this.apiName });

  getById = (id: string) =>
    this.restService.request<any, DistrictDto>({
      method: 'GET',
      url: `/apiv1/District/${id}`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
