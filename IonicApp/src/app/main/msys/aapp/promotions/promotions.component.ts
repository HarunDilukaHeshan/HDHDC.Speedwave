import { Component, OnInit } from '@angular/core';
import { PromotionService } from 'src/app/proxy/controllers';
import { PromotionDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotions.component.html',
  styleUrls: ['./promotions.component.scss'],
})
export class PromotionsComponent implements OnInit {

  private dtoArr: Array<PromotionDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private promotionService: PromotionService) {
    this.loadAllPromotionDto();
  }

  ngOnInit() {
    this.loadAllPromotionDto();
  }

  private loadAllPromotionDto() {
    this.promotionService.getAll()
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

    this.promotionService.deleteById(id)
      .subscribe(() => {
        this.loadAllPromotionDto();
      }, err => { });
  }

  private getFormattedDate(str: string) {
    var date = new Date(str);
    return date.toDateString();
  }
}
