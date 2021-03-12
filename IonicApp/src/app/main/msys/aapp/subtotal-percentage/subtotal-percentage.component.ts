import { Component, OnInit } from '@angular/core';
import { SubtotalPercentageService } from 'src/app/proxy/controllers';
import { SubtotalPercentageDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-subtotal-percentage',
  templateUrl: './subtotal-percentage.component.html',
  styleUrls: ['./subtotal-percentage.component.scss'],
})
export class SubtotalPercentageComponent implements OnInit {

  private dtoArr: Array<SubtotalPercentageDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private subtotalPercentageService: SubtotalPercentageService) 
  { }

  ngOnInit() 
  { 
    this.loadAllDto();
  }

  private loadAllDto() {
    this.subtotalPercentageService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => { });
  }

  private async delete(id: number) {
    var alert = await this.commonAlerts.warningAlert(
      '',
      '',
      'Do you want to delete this item?',
      CommonAlertsButtonGroups.YesNo);

    if (alert == CommonAlertsButtons.No) return;

    this.subtotalPercentageService.deleteById(id)
      .subscribe(() => {
        this.loadAllDto();
      }, err => { });
  }
}
