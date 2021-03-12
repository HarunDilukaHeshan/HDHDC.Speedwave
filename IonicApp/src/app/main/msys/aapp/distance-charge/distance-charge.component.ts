import { Component, OnInit } from '@angular/core';
import { DistanceChargeService } from 'src/app/proxy/controllers';
import { DistanceChargeDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-distance-charge',
  templateUrl: './distance-charge.component.html',
  styleUrls: ['./distance-charge.component.scss'],
})
export class DistanceChargeComponent implements OnInit {

  private dtoArr: Array<DistanceChargeDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private distanceChargeService: DistanceChargeService) 
  { }

  ngOnInit() {
    this.loadAllDistanceChargeDto();
  }

  private loadAllDistanceChargeDto() {
    this.distanceChargeService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => {

      });
  }

  private async delete(id: number) {
    var alert = await this.commonAlerts.warningAlert(
      '',
      '',
      'Do you want to delete this item?',
      CommonAlertsButtonGroups.YesNo);

    if (alert == CommonAlertsButtons.No) return;

    this.distanceChargeService.deleteById(id)
      .subscribe(() => {
        this.loadAllDistanceChargeDto();
      }, err => { });
  }
}
