import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { PromotionCreateDto, PromotionDto, PromotionUpdateDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class PromotionService {
  apiName = 'Default';

  createByDto = (dto: PromotionCreateDto) =>
    this.restService.request<any, PromotionDto>({
      method: 'POST',
      url: `/apiv1/Promotion`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Promotion/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, PromotionDto[]>({
      method: 'GET',
      url: `/apiv1/Promotion`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, PromotionDto>({
      method: 'GET',
      url: `/apiv1/Promotion/${id}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: PromotionUpdateDto) =>
    this.restService.request<any, PromotionDto>({
      method: 'PUT',
      url: `/apiv1/Promotion/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
