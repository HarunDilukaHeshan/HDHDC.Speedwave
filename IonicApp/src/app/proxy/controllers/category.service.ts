import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CategoryCreateDto, CategoryDto, CategoryUpdateDto, ThumbnailDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  apiName = 'Default';

  createByDto = (dto: CategoryCreateDto) =>
    this.restService.request<any, CategoryDto>({
      method: 'POST',
      url: `/apiv1/Category`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Category/${id}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, CategoryDto[]>({
      method: 'GET',
      url: `/apiv1/Category`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, CategoryDto>({
      method: 'GET',
      url: `/apiv1/Category/${id}`,
    },
    { apiName: this.apiName });

  getThumbnailByThumbnail = (thumbnail: string) =>
    this.restService.request<any, ThumbnailDto>({
      method: 'GET',
      url: `/apiv1/Category/thumbnail/${thumbnail}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: CategoryUpdateDto) =>
    this.restService.request<any, CategoryDto>({
      method: 'PUT',
      url: `/apiv1/Category/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
