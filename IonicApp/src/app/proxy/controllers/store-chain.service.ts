import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { StoreChainCreateDto, StoreChainDto, StoreChainUpdateDto, ThumbnailDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class StoreChainService {
  apiName = 'Default';

  createByDto = (dto: StoreChainCreateDto) =>
    this.restService.request<any, StoreChainDto>({
      method: 'POST',
      url: `/apiv1/StoreChain`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/StoreChain/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, StoreChainDto[]>({
      method: 'GET',
      url: `/apiv1/StoreChain`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, StoreChainDto>({
      method: 'GET',
      url: `/apiv1/StoreChain/${id}`,
    },
    { apiName: this.apiName });

  getLogoById = (id: number) =>
    this.restService.request<any, ThumbnailDto>({
      method: 'GET',
      url: `/apiv1/StoreChain/${id}/logo`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: StoreChainUpdateDto) =>
    this.restService.request<any, StoreChainDto>({
      method: 'PUT',
      url: `/apiv1/StoreChain/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  updateLogoByIdAndDto = (id: number, dto: ThumbnailDto) =>
    this.restService.request<any, StoreChainDto>({
      method: 'PUT',
      url: `/apiv1/StoreChain/${id}/logo`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
