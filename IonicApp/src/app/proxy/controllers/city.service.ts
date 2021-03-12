import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CityCreateDto, CityDto, CityUpdateDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  apiName = 'Default';

  createByDto = (dto: CityCreateDto) =>
    this.restService.request<any, CityDto>({
      method: 'POST',
      url: `/apiv1/City`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/City/${id}`,
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, CityDto>({
      method: 'GET',
      url: `/apiv1/City/${id}`,
    },
    { apiName: this.apiName });

  searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId = (skipCount?: number, maxResultCount: number = 10, cityId?: number, keyword?: string, districtId?: string) =>
    this.restService.request<any, CityDto[]>({
      method: 'GET',
      url: `/apiv1/City`,
      params: { skipCount: skipCount, maxResultCount: maxResultCount, cityId: cityId, keyword: keyword, districtId: districtId },
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: CityUpdateDto) =>
    this.restService.request<any, CityDto>({
      method: 'PUT',
      url: `/apiv1/City/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
