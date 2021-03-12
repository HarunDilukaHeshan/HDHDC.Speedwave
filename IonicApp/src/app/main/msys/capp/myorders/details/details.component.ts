import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OrderService } from 'src/app/proxy/controllers';
import { OrderDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss'],
})
export class DetailsComponent implements OnInit {

  private dto: OrderDto;

  constructor(
    private commonAlerts: CommonAlerts,
    private navCtrl: NavController,
    private activatedRoute: ActivatedRoute,
    private orderService: OrderService) 
  { }

  ngOnInit() { 
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['id'];
        if (id && !isNaN(id))
          this.loadOrderDto(id);
        else
          throw Error('Id not found');
      });
  }

  private async removeOrder(id: number) {
    var result = await this.commonAlerts.warningAlert('', '', 'Do you want to remove this order?', 
      CommonAlertsButtonGroups.YesNo);
    
      if (result == CommonAlertsButtons.No) return;

    this.orderService.removeById(id)
      .subscribe(() => {
        this.navCtrl.back();
      }, err => {

      });
  }

  private loadOrderDto(id: number) {
    this.orderService.getById(id)
      .subscribe(dto => {
        this.dto = dto;
        console.log(dto);
      }, err => {
        throw Error('Order not found');
      });
  }
}
