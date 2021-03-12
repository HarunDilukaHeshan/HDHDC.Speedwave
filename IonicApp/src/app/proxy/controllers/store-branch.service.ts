import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { DayOfTheWeek } from 'src/app/shared/DayOfTheWeek';
import type { ItemStoreBranchDto, StoreBranchCreateDto, StoreBranchDto, StoreBranchUpdateDto, StoreClosingDateDto, StoreOpenDayDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class StoreBranchService {
  apiName = 'Default';

  addItemByIdAndDto = (id: number, dto: ItemStoreBranchDto) =>
    this.restService.request<any, ItemStoreBranchDto>({
      method: 'POST',
      url: `/apiv1/StoreBranch/${id}/items`,
      body: dto,
    },
    { apiName: this.apiName });

  createByDto = (dto: StoreBranchCreateDto) =>
    this.restService.request<any, StoreBranchDto>({
      method: 'POST',
      url: `/apiv1/StoreBranch`,
      body: dto,
    },
    { apiName: this.apiName });

  createClosingDateByIdAndDto = (id: number, dto: StoreClosingDateDto) =>
    this.restService.request<any, StoreClosingDateDto>({
      method: 'POST',
      url: `/apiv1/StoreBranch/${id}/closingdate`,
      body: dto,
    },
    { apiName: this.apiName });

  createOpenDayByIdAndDto = (id: number, dto: StoreOpenDayDto) =>
    this.restService.request<any, StoreOpenDayDto>({
      method: 'POST',
      url: `/apiv1/StoreBranch/${id}/openingdays`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/StoreBranch/${id}`,
    },
    { apiName: this.apiName });

  deleteClosingDateByIdAndClosingdateid = (id: number, closingdateid: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/StoreBranch/${id}/closingdate/${closingdateid}`,
    },
    { apiName: this.apiName });

  getAllByCityId = (cityId: number) =>
    this.restService.request<any, StoreBranchDto[]>({
      method: 'GET',
      url: `/apiv1/StoreBranch`,
      params: { cityId: cityId },
    },
    { apiName: this.apiName });

  getAllClosingDatesById = (id: number) =>
    this.restService.request<any, StoreClosingDateDto[]>({
      method: 'GET',
      url: `/apiv1/StoreBranch/${id}/closingdate`,
    },
    { apiName: this.apiName });

  getAllItemsByIdAndKeywordsAndSkipCountAndMaxResultCountAndIncludeDetails = (id: number, keywords: string, skipCount?: number, maxResultCount: number = 10, includeDetails?: boolean) =>
    this.restService.request<any, ItemStoreBranchDto[]>({
      method: 'GET',
      url: `/apiv1/StoreBranch/${id}/items`,
      params: { keywords: keywords, skipCount: skipCount, maxResultCount: maxResultCount, includeDetails: includeDetails },
    },
    { apiName: this.apiName });

  getAllOpenDayDtoById = (id: number) =>
    this.restService.request<any, StoreOpenDayDto[]>({
      method: 'GET',
      url: `/apiv1/StoreBranch/${id}/openingdays`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, StoreBranchDto>({
      method: 'GET',
      url: `/apiv1/StoreBranch/${id}`,
    },
    { apiName: this.apiName });

  getOpenDayDtoByIdAndDofw = (id: number, dofw: DayOfTheWeek) =>
    this.restService.request<any, StoreOpenDayDto>({
      method: 'GET',
      url: `/apiv1/StoreBranch/${id}/openingdays/${dofw}`,
    },
    { apiName: this.apiName });

  removeItemByIdAndItemId = (id: number, itemId: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/StoreBranch/${id}/items/${itemId}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: StoreBranchUpdateDto) =>
    this.restService.request<any, StoreBranchDto>({
      method: 'PUT',
      url: `/apiv1/StoreBranch/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  updateOpenDayByIdAndDtoArr = (id: number, dtoArr: StoreOpenDayDto[]) =>
    this.restService.request<any, StoreOpenDayDto[]>({
      method: 'PUT',
      url: `/apiv1/StoreBranch/${id}/openingdays`,
      body: dtoArr,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
