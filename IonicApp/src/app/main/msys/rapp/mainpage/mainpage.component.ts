import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/proxy/controllers';
import { OAuthService } from 'angular-oauth2-oidc';
import { OrderDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.scss'],
})
export class MainpageComponent implements OnInit {

  private orderDtoArr: Array<OrderDto> = [];
  private selectedDtoArr: Array<OrderDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private oAuthService: OAuthService,
    private orderService: OrderService) 
  { }

  ngOnInit() 
  { 
    this.init();
  }

  private async login() {
    await this.oAuthService.loadDiscoveryDocument();
    await this.oAuthService.fetchTokenUsingPasswordFlow('RiderThree', 'riderThree/03');
  }

  private async init() {
    await this.login();
    this.loadAllActiveOrderDto();
    this.loadSelectedAllOrderDto();
  }

  private async Deselect(id: number) {
    var result = await this.commonAlerts.warningAlert('',
      'Warning',
      'Do you want to perform this operation?',
      CommonAlertsButtonGroups.YesNo);

    if (result == CommonAlertsButtons.Yes)
      this.orderService.deselectById(id)
        .subscribe(() => {
          console.log('Deselected');
          this.init();
        }, err => { });
  }

  private loadAllActiveOrderDto() {
    this.orderService.getOrdersByTypeAndCityIdAndSkipCountAndMaxResultCount('active')
      .subscribe(dtoArr => {
        this.orderDtoArr = dtoArr;
        console.log(dtoArr);
      })
  }

  private loadSelectedAllOrderDto() {
    this.orderService.getOrdersByTypeAndCityIdAndSkipCountAndMaxResultCount('selected')
      .subscribe(dtoArr => {
        this.selectedDtoArr = dtoArr;
        console.log(dtoArr);
      })
  }
}
