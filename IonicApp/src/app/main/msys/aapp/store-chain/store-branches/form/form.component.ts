import { Component, OnInit, ViewChild } from '@angular/core';
import { async } from '@angular/core/testing';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IonSlides, NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { StoreBranchService, StoreChainService } from 'src/app/proxy/controllers';
import { StoreBranchCreateDto, StoreBranchDto, StoreBranchUpdateDto, StoreChainDto, StoreOpenDayDto } from 'src/app/proxy/dtos';
import { FormStatusHolder } from './basic-info/basic-info.component';
import { DayoftheweekComponent } from './opening-days/dayoftheweek/dayoftheweek.component';
import { DayOfWeekHolder } from './opening-days/opening-days.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isUpdatingForm: boolean = false;

  private isWaiting: boolean = false;

  private isValidForm: boolean = false;

  private openDayDtoArr: Array<StoreOpenDayDto> = [];

  private storeChainDto: StoreChainDto;

  private storeBranchDto: StoreBranchDto;

  private sbcDto: StoreBranchCreateDto;

  private dayOfWeekHolderArr: Array<DayOfWeekHolder>;

  @ViewChild('slideComp')
  private slidesPages: IonSlides;

  constructor(
    private storeBranchService: StoreBranchService,
    private storeChainService: StoreChainService,
    private navController: NavController,
    private activatedRoute: ActivatedRoute) 
  { }

  ngOnInit(): void 
  { 
    this.init();
  }

  private slidesCtrl = {
    onSlideChange: async (ev: CustomEvent) => {
      this.slidesCtrl.isBeginning = await this.slidesPages.isBeginning();
      this.slidesCtrl.isEnd = await this.slidesPages.isEnd();
    },
    isEnd: false,
    isBeginning: true,
    options: {
      allowTouchMove: false
    }
  };

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var chainId = params['storeChainId'];
        var branchId = params['branchId'];

        if (chainId && !isNaN(chainId)) {
          this.loadChainDto(chainId);
        }
        else {
          this.navController.back();
        }

        if (branchId && !isNaN(branchId)) {
          this.isUpdatingForm = true;
          this.loadBranchDto(branchId);
          this.loadOpenDayDtoList(branchId);
        }
      });
  }

  private loadChainDto(id: number) {
    this.storeChainService.getById(id)
      .subscribe(dto => {
        this.storeChainDto = dto;
      }, err => {
        this.navController.back();
        throw Error('Invalid storeChainId');
      });
  }

  private loadBranchDto(id: number) {
    this.storeBranchService.getById(id)
      .subscribe(dto => {
        this.storeBranchDto = dto;
      }, err => {
        this.navController.back();
        throw Error('Invalid storeBranchId');
      });
  }

  private loadOpenDayDtoList(id: number) {
    this.storeBranchService.getAllOpenDayDtoById(id)
      .subscribe(dtoList => {
        this.openDayDtoArr = dtoList;
      }, err => {
        this.navController.back();
        throw Error('There is an issue loading openday dtos');
      });
  }

  private onFormStatusChange(ev: CustomEvent<FormStatusHolder>)
  {
    if (ev.detail.isValid) {
      this.isValidForm = true;
      this.sbcDto = ev.detail.dto;
    }
  }

  private onOpenDayReady(ev: CustomEvent<DayOfWeekHolder[]>)
  {
    this.dayOfWeekHolderArr = ev.detail;
  }

  private onOpenDayChange(ev: CustomEvent<DayOfWeekHolder[]>)
  {
    this.dayOfWeekHolderArr = ev.detail;
  }

  private save()
  {
    if (this.isWaiting) return;

    this.isWaiting = true;
    if (this.isUpdatingForm)
      this.update();
    else
      this.create();
  }

  private create() {
    var dto = this.sbcDto;
    this.storeBranchService.createByDto(dto)
      .subscribe(data => {
        this.updateOpenDayDtoList();
        alert('created');        
        this.isWaiting = false;
      }, err => {
        this.isWaiting = false;
      });
  }

  private update()
  {
    var dto = this.getUpdateDto();
    this.storeBranchService.updateByIdAndDto(this.storeBranchDto.id, dto)
      .subscribe(data => {
        this.updateOpenDayDtoList();
        alert('updated');        
      }, err => {
        this.isWaiting = false;
      });
  }

  private updateOpenDayDtoList()
  {
    var dtoList = this.getOpenDayDtoList();
    this.storeBranchService.updateOpenDayByIdAndDtoArr(this.storeBranchDto.id, dtoList)
      .subscribe(data => {  
        this.isWaiting = false;      
      }, err => {
        this.isWaiting = false;
      });
  }

  private getOpenDayDtoList()
  {
    var dtoList = new Array<StoreOpenDayDto>();
    this.dayOfWeekHolderArr.forEach(holder => {
      if (holder.disabled) return;
      var dto = {} as StoreOpenDayDto;
      dto.storeBranchID = this.storeBranchDto.id;
      dto.dayOfWeek = holder.dayOfWeek;
      dto.openingTime = holder.openingTime;
      dto.closingTime = holder.closingTime;
      dtoList.push(dto);
    });

    return dtoList;
  }

  private getUpdateDto()
  {
    var dto = {} as StoreBranchUpdateDto;
    dto.geolocation = this.sbcDto.geolocation;
    dto.contactNo01 = this.sbcDto.contactNo01;
    dto.contactNo02 = this.sbcDto.contactNo02;
    return dto;
  }
}
