import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { SubtotalPercentageCreateUpdateDto, SubtotalPercentageDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class SubtotalPercentageService {
  apiName = 'Default';

  createByDto = (dto: SubtotalPercentageCreateUpdateDto) =>
    this.restService.request<any, SubtotalPercentageDto>({
      method: 'POST',
      url: `/apiv1/SubtotalPercentage`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/SubtotalPercentage/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, SubtotalPercentageDto[]>({
      method: 'GET',
      url: `/apiv1/SubtotalPercentage`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, SubtotalPercentageDto>({
      method: 'GET',
      url: `/apiv1/SubtotalPercentage/${id}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: SubtotalPercentageCreateUpdateDto) =>
    this.restService.request<any, SubtotalPercentageDto>({
      method: 'PUT',
      url: `/apiv1/SubtotalPercentage/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
