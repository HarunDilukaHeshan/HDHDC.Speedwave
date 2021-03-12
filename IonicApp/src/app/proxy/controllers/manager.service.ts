import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppUserDto, ManagerCreateDto, ManagerDto, ManagerUpdateDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ManagerService {
  apiName = 'Default';

  createByDto = (dto: ManagerCreateDto) =>
    this.restService.request<any, AppUserDto>({
      method: 'POST',
      url: `/apiv1/Manager`,
      body: dto,
    },
    { apiName: this.apiName });

  getAllByDistrictId = (districtId?: string) =>
    this.restService.request<any, ManagerDto[]>({
      method: 'GET',
      url: `/apiv1/Manager`,
      params: { districtId: districtId },
    },
    { apiName: this.apiName });

  getById = (id: number) =>
    this.restService.request<any, ManagerDto>({
      method: 'GET',
      url: `/apiv1/Manager/${id}`,
    },
    { apiName: this.apiName });

  getByUserId = (userId: string) =>
    this.restService.request<any, ManagerDto>({
      method: 'GET',
      url: `/user/${userId}`,
    },
    { apiName: this.apiName });

  updateByIdAndDto = (id: number, dto: ManagerUpdateDto) =>
    this.restService.request<any, ManagerDto>({
      method: 'PUT',
      url: `/apiv1/Manager/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  updateStatusByIdAndStatusId = (id: number, statusId: string) =>
    this.restService.request<any, ManagerDto>({
      method: 'PUT',
      url: `/apiv1/Manager/${id}/status`,
      params: { statusId: statusId },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
