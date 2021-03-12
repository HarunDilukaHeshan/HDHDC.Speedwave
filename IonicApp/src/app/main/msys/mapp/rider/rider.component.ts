import { Component, OnInit } from '@angular/core';
import { ActionSheetController } from '@ionic/angular';
import { RiderService } from 'src/app/proxy/controllers';
import { RiderDto } from 'src/app/proxy/dtos';
import { EntityStatusConsts } from 'src/app/shared/constants/entity-status-consts';

@Component({
  selector: 'app-rider',
  templateUrl: './rider.component.html',
  styleUrls: ['./rider.component.scss'],
})
export class RiderComponent implements OnInit {

  private dtoArr: Array<RiderDto> = [];

  constructor(
    private actionSheetController: ActionSheetController,
    private riderService: RiderService) 
  { }

  ngOnInit() 
  { 
    this.onSearchbarChange(new CustomEvent('onInit', { detail: { value: '' } }));
  }

  private loadAllRiderDto() {
   // this.riderService.searchByWordsAndSkipCountAndMaxResultCount()
  } 

  private onSearchbarChange(ev: CustomEvent) {
    var value = ev.detail.value;
    this.riderService.searchByWordsAndSkipCountAndMaxResultCount(value, 0, 10)
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;     
        console.log(dtoArr);   
      }, err => {});
  }

  private async open(id: number) {
    var asc = await this.actionSheetController.create({
      header: 'Change rider status',
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
    this.riderService.updateStatusByIdAndStatusId(id, status)
      .subscribe(dto => {
        this.loadAllRiderDto();
      }, err => {});
  }
}
