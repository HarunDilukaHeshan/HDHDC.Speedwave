import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { CityService, StoreBranchService, StoreChainService } from 'src/app/proxy/controllers';
import { CityDto, StoreBranchDto, StoreChainDto, StoreClosingDateDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-closing-dates',
  templateUrl: './closing-dates.component.html',
  styleUrls: ['./closing-dates.component.scss'],
})
export class ClosingDatesComponent implements OnInit {
  
  private chainDto: StoreChainDto;

  private branchDto: StoreBranchDto;

  private cityDto: CityDto;

  private closingDateDtoArr: Array<StoreClosingDateDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private cityService: CityService,
    private activatedRoute: ActivatedRoute,
    private storeBranchService: StoreBranchService,
    private storeChainService: StoreChainService,
    private navController: NavController) 
  { }

  ngOnInit() {
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var chainId = params['storeChainId'];
        var branchId = params['storeBranchId'];

        if (chainId && !isNaN(chainId))
          this.loadStoreChainDto(chainId);
        else
          throw Error('StoreChainId cannot be empty');

        if (branchId && !isNaN(branchId))
          { this.loadStoreBranchId(branchId);
            this.loadAllClosingDateDto(branchId); }
        else
          throw Error('StoreBranchId cannot be empty');
      });
  }

  private loadStoreChainDto(id: number) {
    this.storeChainService.getById(id)
      .subscribe(dto => {
        this.chainDto = dto;
      }, err => {
        this.navController.back();
        throw Error('Invalid storeChainId');
      });
  }

  private loadStoreBranchId(id: number) {
    this.storeBranchService.getById(id)
      .subscribe(dto => {
        this.branchDto = dto;
        this.loadCityDto(dto.cityID);
      }, err => {
        this.navController.back();
        throw Error('Invalid storeChainId');
      });
  }

  private loadAllClosingDateDto(id: number) {
    this.storeBranchService.getAllClosingDatesById(id)
      .subscribe(dtoList => {
        this.closingDateDtoArr = dtoList;
      }, err => {
        this.navController.back();
        throw Error('An unexpected error occured');
      });
  }

  private loadCityDto(id: number) {
    this.cityService.getById(id)
      .subscribe(dto => {
        this.cityDto = dto;
      }, err => {
        this.navController.back();
        throw Error('An unexpected error occured');
      });
  }

  private async delete(closingDateId: number, branchId: number) {

    var alert = await this.commonAlerts.warningAlert(
      '', 
      '', 
      'Do you want to delete this item?', 
      CommonAlertsButtonGroups.YesNo);
    
    if (alert == CommonAlertsButtons.No) return;      

    this.storeBranchService.deleteClosingDateByIdAndClosingdateid(branchId, closingDateId)
      .subscribe(() => {
        console.log(closingDateId);
        this.loadAllClosingDateDto(branchId);
      }, err => {
        throw Error('An unexpected error occured');
      });
  }
}
