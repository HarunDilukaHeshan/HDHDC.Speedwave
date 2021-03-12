import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppUserDto, CityDto, RiderCreateDto, RiderDto, RiderUpdateDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class RiderService {
  apiName = 'Default';

  createByDto = (dto: RiderCreateDto) =>
    this.restService.request<any, AppUserDto>({
      method: 'POST',
      url: `/apiv1/Rider`,
      body: dto,
    },
    { apiName: this.apiName });

  getAllByCityIdAndSkipCountAndMaxResultCount = (cityId: number = -1, skipCount?: number, maxResultCount: number = 10) =>
    this.restService.request<any, RiderDto[]>({
      method: 'GET',
      url: `/apiv1/Rider`,
      params: { cityId: cityId, skipCount: skipCount, maxResultCount: maxResultCount },
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, RiderDto>({
      method: 'GET',
      url: `/apiv1/Rider/${id}`,
    },
    { apiName: this.apiName });

  getCurrentRider = () =>
    this.restService.request<any, RiderDto>({
      method: 'GET',
      url: `/apiv1/Rider/current`,
    },
    { apiName: this.apiName });

  getRiderCoverage = () =>
    this.restService.request<any, CityDto[]>({
      method: 'GET',
      url: `/apiv1/Rider/current/coverage`,
    },
    { apiName: this.apiName });

  searchByWordsAndSkipCountAndMaxResultCount = (words: string, skipCount?: number, maxResultCount: number = 10) =>
    this.restService.request<any, RiderDto[]>({
      method: 'GET',
      url: `/apiv1/Rider/search`,
      params: { words: words, skipCount: skipCount, maxResultCount: maxResultCount },
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: RiderUpdateDto) =>
    this.restService.request<any, RiderDto>({
      method: 'PUT',
      url: `/apiv1/Rider/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  updateRiderCoverageByCityIds = (cityIds: number[]) =>
    this.restService.request<any, CityDto[]>({
      method: 'PUT',
      url: `/apiv1/Rider/current/coverage`,
      params: { cityIds: cityIds },
    },
    { apiName: this.apiName });

  updateStatusByIdAndStatusId = (id: number, statusId: string) =>
    this.restService.request<any, RiderDto>({
      method: 'PUT',
      url: `/apiv1/Rider/${id}/status`,
      params: { statusId: statusId },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
