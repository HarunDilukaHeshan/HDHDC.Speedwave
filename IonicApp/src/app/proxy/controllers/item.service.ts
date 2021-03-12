import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BlobFileDto, ItemCategoryDto, ItemCreateDto, ItemDto, ItemUpdateDto, ThumbnailDto } from '../dtos/models';
import type { ItemCategoryKey } from '../speedwave-app-services/models';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  apiName = 'Default';

  addItemPicByIdAndDto = (id: number, dto: BlobFileDto) =>
    this.restService.request<any, BlobFileDto>({
      method: 'POST',
      url: `/apiv1/Item/${id}/itempic`,
      body: dto,
    },
    { apiName: this.apiName });

  createByDto = (dto: ItemCreateDto) =>
    this.restService.request<any, ItemDto>({
      method: 'POST',
      url: `/apiv1/Item`,
      body: dto,
    },
    { apiName: this.apiName });

  createItemCategoryByDto = (dto: ItemCategoryDto) =>
    this.restService.request<any, ItemCategoryDto>({
      method: 'POST',
      url: `/apiv1/Item/category`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Item/${id}`,
    },
    { apiName: this.apiName });

  deleteItemCategoryByKey = (key: ItemCategoryKey) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Item/category`,
      body: key,
    },
    { apiName: this.apiName });

  deleteItemPicByIdAndFilename = (id: number, filename: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/Item/${id}/itempic/${filename}`,
    },
    { apiName: this.apiName });

  getAllItemCategoryById = (id: number) =>
    this.restService.request<any, ItemCategoryDto[]>({
      method: 'GET',
      url: `/apiv1/Item/${id}/itemcategory`,
    },
    { apiName: this.apiName });

  getAllItemPicById = (id: number) =>
    this.restService.request<any, BlobFileDto[]>({
      method: 'GET',
      url: `/apiv1/Item/${id}/itempic`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, ItemDto>({
      method: 'GET',
      url: `/apiv1/Item/${id}`,
    },
    { apiName: this.apiName });

  getListByKeywordsAndSeedAndSkipCountAndMaxResultCountAndItemIdsArr = (keywords?: string, seed?: number, skipCount?: number, maxResultCount: number = 10, itemIdsArr?: number[]) =>
    this.restService.request<any, ItemDto[]>({
      method: 'GET',
      url: `/apiv1/Item`,
      params: { keywords: keywords, seed: seed, skipCount: skipCount, maxResultCount: maxResultCount, itemIdsArr: itemIdsArr },
    },
    { apiName: this.apiName });

  getThumbnailByItempic = (itempic: string) =>
    this.restService.request<any, ThumbnailDto>({
      method: 'GET',
      url: `/apiv1/Item/itempic/${itempic}`,
    },
    { apiName: this.apiName });

  search = (keywords?: string, skipCount?: number, maxResultCount: number = 10) =>
    this.restService.request<any, ItemDto[]>({
      method: 'GET',
      url: `/apiv1/Item/search`,
      params: { keywords: keywords, skipCount: skipCount, maxResultCount: maxResultCount },
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: ItemUpdateDto) =>
    this.restService.request<any, ItemDto>({
      method: 'PUT',
      url: `/apiv1/Item/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  updateItemCategoryByIdAndDtosArr = (id: number, dtosArr: ItemCategoryDto[]) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/apiv1/Item/${id}/itemcategory`,
      body: dtosArr,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
