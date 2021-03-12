import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppUserDto, PasswordResetDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiName = 'Default';

  get = () =>
    this.restService.request<any, AppUserDto>({
      method: 'GET',
      url: `/apiv1/User/current`,
    },
    { apiName: this.apiName });

  getByUsername = (username: string) =>
    this.restService.request<any, void>({
      method: 'GET',
      url: `/apiv1/User/username/${username}`,
    },
    { apiName: this.apiName });

  resetPasswordByDto = (dto: PasswordResetDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/apiv1/User/password-reset`,
      body: dto,
    },
    { apiName: this.apiName });

  sendPasswordResetCodeByDto = (dto: PasswordResetDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/apiv1/User/password-reset-code`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
