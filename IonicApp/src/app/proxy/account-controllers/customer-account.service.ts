import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AddressCreateDto, AddressDto, AddressUpdateDto, AppUserDto, UserCreateDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CustomerAccountService {
  apiName = 'Default';

  createAddressByDto = (dto: AddressCreateDto) =>
    this.restService.request<any, AddressDto>({
      method: 'POST',
      url: `/apiv1/CustomerAccount/address`,
      body: dto,
    },
    { apiName: this.apiName });

  createByDto = (dto: UserCreateDto) =>
    this.restService.request<any, AppUserDto>({
      method: 'POST',
      url: `/apiv1/CustomerAccount`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteAddressById = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/CustomerAccount/address/${id}`,
    },
    { apiName: this.apiName });

  getAddressById = (id: number) =>
    this.restService.request<any, AddressDto>({
      method: 'GET',
      url: `/apiv1/CustomerAccount/address/${id}`,
    },
    { apiName: this.apiName });

  getAllAddress = () =>
    this.restService.request<any, AddressDto[]>({
      method: 'GET',
      url: `/apiv1/CustomerAccount/address`,
    },
    { apiName: this.apiName });

  updateAddressByIdAndDto = (id: number, dto: AddressUpdateDto) =>
    this.restService.request<any, AddressDto>({
      method: 'PUT',
      url: `/apiv1/CustomerAccount/address/${id}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
