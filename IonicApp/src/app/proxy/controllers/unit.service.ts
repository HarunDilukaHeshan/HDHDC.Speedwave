import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { UnitDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class UnitService {
  apiName = 'Default';

  createByDto = (dto: UnitDto) =>
    this.restService.request<any, UnitDto>({
      method: 'POST',
      url: `/apiv1/Unit`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Unit/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, UnitDto[]>({
      method: 'GET',
      url: `/apiv1/Unit`,
    },
    { apiName: this.apiName });

  getById = (id: string) =>
    this.restService.request<any, UnitDto>({
      method: 'GET',
      url: `/apiv1/Unit/${id}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: string, dto: UnitDto) =>
    this.restService.request<any, UnitDto>({
      method: 'PUT',
      url: `/apiv1/Unit/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
