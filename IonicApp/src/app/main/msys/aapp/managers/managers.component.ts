import { Component, OnInit } from '@angular/core';
import { ActionSheetController } from '@ionic/angular';
import { ManagerService } from 'src/app/proxy/controllers';
import { ManagerDto } from 'src/app/proxy/dtos';
import { EntityStatusConsts } from 'src/app/shared/constants/entity-status-consts';

@Component({
  selector: 'app-managers',
  templateUrl: './managers.component.html',
  styleUrls: ['./managers.component.scss'],
})
export class ManagersComponent implements OnInit {

  private dtoArr: Array<ManagerDto> = [];

  constructor(
    private actionSheetController: ActionSheetController,
    private managerService: ManagerService) 
  { }

  ngOnInit() {
    this.loadAllManager();
  }

  private loadAllManager() {
    this.managerService.getAllByDistrictId()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => {});
  }

  private async open(id: number) {
    var asc = await this.actionSheetController.create({
      header: 'Change manager status',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'First warning',
          role: EntityStatusConsts.WarningOne          
        },
        {
          text: 'Second warning',
          role: EntityStatusConsts.WarningTwo
        },
        {
          text: 'Block',
          role: EntityStatusConsts.Blocked
        }
      ]
    });        

    asc.onDidDismiss().then(data => {
      this.changeStatus(id, data.role);
    });

    await asc.present()
  }

  private changeStatus(id: number, status: string) {
    this.managerService.updateStatusByIdAndStatusId(id, status)
      .subscribe(dto => {
        this.loadAllManager();
      }, err => {});
  }
}
