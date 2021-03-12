import { Component, OnInit } from '@angular/core';
import { ProvinceDto } from 'src/app/proxy/dtos';
import { ProvinceService } from 'src/app/proxy/controllers';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.scss'],
})
export class ProvinceComponent implements OnInit {

  private provinceDtoArr: Array<ProvinceDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private provinceService: ProvinceService) 
  { }

  ngOnInit() 
  { 
    this.loadAllProvinceDto();
  }

  private loadAllProvinceDto()
  {
    this.provinceService.getAll()
      .subscribe(data => {
        this.provinceDtoArr = data;        
      }, err => { });
  }

  private async delete(id: string)
  {
    var result = await this.commonAlerts.warningAlert(
      '', 
      'Warning', 
      'Do you want to delete the selected item?', 
      CommonAlertsButtonGroups.YesNo);

    if (result == CommonAlertsButtons.Yes)
      this.provinceService.deleteById(id)
        .subscribe(() => {
          this.loadAllProvinceDto();
        }, err => {
          this.loadAllProvinceDto();
        });
  }

}
