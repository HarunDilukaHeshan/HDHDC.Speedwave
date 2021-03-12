import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { SlideShowPictureDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class SlideShowService {
  apiName = 'Default';

  createByDto = (dto: SlideShowPictureDto) =>
    this.restService.request<any, SlideShowPictureDto>({
      method: 'POST',
      url: `/apiv1/SlideShow`,
      body: dto,
    },
    { apiName: this.apiName });

  deleteByFileName = (fileName: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/apiv1/SlideShow/${fileName}`,
    },
    { apiName: this.apiName });

  getAll = () =>
    this.restService.request<any, SlideShowPictureDto[]>({
      method: 'GET',
      url: `/apiv1/SlideShow`,
    },
    { apiName: this.apiName });

  getByFileName = (fileName: string) =>
    this.restService.request<any, SlideShowPictureDto>({
      method: 'GET',
      url: `/apiv1/SlideShow/${fileName}`,
    },
    { apiName: this.apiName });

  updateByFileNameAndDto = (fileName: string, dto: SlideShowPictureDto) =>
    this.restService.request<any, SlideShowPictureDto>({
      method: 'PUT',
      url: `/apiv1/SlideShow/${fileName}`,
      body: dto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
