import { Component, OnInit } from '@angular/core';
import { CustomerAccountService } from 'src/app/proxy/account-controllers';
import { AddressDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-customer-address',
  templateUrl: './customer-address.component.html',
  styleUrls: ['./customer-address.component.scss'],
})
export class CustomerAddressComponent implements OnInit {

  private dtoArr: Array<AddressDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private customerService: CustomerAccountService) 
  { }

  ngOnInit() {
    this.loadAllAddressDto();
  }

  private loadAllAddressDto() {
    this.customerService.getAllAddress()
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

    var subs = this.customerService.deleteAddressById(id)
      .subscribe(() => {
        this.loadAllAddressDto();                
        subs.unsubscribe();
      }, err => {
        this.loadAllAddressDto();
        subs.unsubscribe();
      });
  }
}
