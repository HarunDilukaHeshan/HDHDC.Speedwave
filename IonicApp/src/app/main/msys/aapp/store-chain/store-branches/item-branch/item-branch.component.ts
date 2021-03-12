import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { from } from 'rxjs';
import { AsyncScheduler } from 'rxjs/internal/scheduler/AsyncScheduler';
import { ItemService, StoreBranchService, StoreChainService } from 'src/app/proxy/controllers';
import { ItemDto, ItemStoreBranchDto, StoreBranchDto, StoreChainDto } from 'src/app/proxy/dtos';
import { SpeedySelectController, SpeedySelectData, SpeedySelectElement, SpeedySelectItem } from 'src/app/shared/components/speedy-select/speedy-select.controller.service';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-item-branch',
  templateUrl: './item-branch.component.html',
  styleUrls: ['./item-branch.component.scss'],
})
export class ItemBranchComponent implements OnInit {

  private dtoArr: Array<ItemStoreBranchDto> = [];
  private chainDto: StoreChainDto;
  private branchDto: StoreBranchDto;

  private keywords: string = '';
  private skipCount: number = 0;
  private maxResultCount: number = 10;

  constructor(
    private commonAlerts: CommonAlerts,
    private navCtrl: NavController,
    private activatedRoute: ActivatedRoute,
    private chainService: StoreChainService,
    private branchService: StoreBranchService) 
  { }

  ngOnInit() { 
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var chainId = params['storeChainId'];
        var branchId = params['storeBranchId'];

        if (!(chainId && branchId && !isNaN(chainId) && !isNaN(chainId))) {
          this.navCtrl.back();
          throw new Error('Invalid params');
        }

        this.loadChainDto(chainId);
        this.loadBranchDto(branchId);
        this.loadAllItemBranchDto(branchId);
      });
  }

  private loadChainDto(chainId: number) {
    this.chainService.getById(chainId)
      .subscribe(dto => {
        this.chainDto = dto;
      }, err => {
        this.navCtrl.back();
        throw Error('Invalid store chain');
      });
  }
  
  private loadBranchDto(branchId: number) {
    this.branchService.getById(branchId)
      .subscribe(dto => {
        this.branchDto = dto;
      }, err => {
        this.navCtrl.back();
        throw Error('Invalid store branch');
      });
  }

  private loadAllItemBranchDto(branchId: number) {

    var ob = this.branchService.getAllItemsByIdAndKeywordsAndSkipCountAndMaxResultCountAndIncludeDetails(branchId, this.keywords, this.skipCount, this.maxResultCount, true);

    ob.subscribe(dtoArr => {
      this.dtoArr = this.dtoArr.concat(dtoArr);
      this.skipCount += dtoArr.length;
    }, err => {
      this.navCtrl.back();
      throw Error('Invalid item branches');
    });

    return from(ob);
  }

  private onSearchbarChange(ev: CustomEvent<{value: string}>) {
    this.keywords = ev.detail.value;
    this.skipCount = 0;    
    this.dtoArr = [];
    this.loadAllItemBranchDto(this.branchDto.id);
  }

  private onScrollReachEnd(ev: CustomEvent) {
    this.loadAllItemBranchDto(this.branchDto.id)
      .subscribe(() => {
        ev.target['complete']();
      }, err => {});    
  }

  private async removeItemBranchDto(itemId: number) {

    var result = await this.commonAlerts.warningAlert('', 'Warning', 'Do you want to delete this item?', CommonAlertsButtonGroups.YesNo);

    if (result == CommonAlertsButtons.Yes)
      this.branchService.removeItemByIdAndItemId(this.branchDto.id, itemId)
        .subscribe(() => { 
          this.skipCount = 0;
          this.dtoArr = [];
          this.loadAllItemBranchDto(this.branchDto.id);
        }, err => { });
  }

  private onSave() {
    this.keywords = '';
    this.skipCount = 0;    
    this.dtoArr = [];
    this.loadAllItemBranchDto(this.branchDto.id);
  }
}
