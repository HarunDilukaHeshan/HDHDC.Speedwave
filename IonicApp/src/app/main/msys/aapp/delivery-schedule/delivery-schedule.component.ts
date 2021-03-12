import { Component, OnInit } from '@angular/core';
import { DeliveryScheduleService } from 'src/app/proxy/controllers';
import { DeliveryScheduleDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-delivery-schedule',
  templateUrl: './delivery-schedule.component.html',
  styleUrls: ['./delivery-schedule.component.scss'],
})
export class DeliveryScheduleComponent implements OnInit {

  private dtoArr: Array<DeliveryScheduleDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private deliveryScheduleService: DeliveryScheduleService) 
  { }

  ngOnInit() {
    this.loadAllDsDto();
  }

  private loadAllDsDto() {
    this.deliveryScheduleService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => { });
  }

  private async delete(id: number)
  {
    var alert = await this.commonAlerts.warningAlert(
      '',
      '',
      'Do you want to delete this item?',
      CommonAlertsButtonGroups.YesNo);

    if (alert == CommonAlertsButtons.No) return;

    this.deliveryScheduleService.deleteById(id)
      .subscribe(() => {
        this.loadAllDsDto();
      }, err => {});
  }
}
