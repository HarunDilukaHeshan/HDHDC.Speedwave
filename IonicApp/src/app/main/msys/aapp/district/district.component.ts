import { Component, OnInit } from '@angular/core';
import { DistrictService } from 'src/app/proxy/controllers';
import { FormGroup } from '@angular/forms';
import { DistrictDto } from 'src/app/proxy/dtos';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.scss'],
})
export class DistrictComponent implements OnInit {

  private isAwaiting: boolean = false;

  private formGroup: FormGroup;

  private districtDtoArr: Array<DistrictDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private districtService: DistrictService) 
  { }

  ngOnInit() {
    this.loadAllDistrictDto();
  }

  private loadAllDistrictDto()
  {
    this.districtService.getAll()
      .subscribe(data => {
        this.districtDtoArr = data;
      }, err => {});
  }

  private async delete(id: string)
  {
    var result = await this.commonAlerts.warningAlert(
      '', 
      'Warning', 
      'Do you want to delete the selected item?', 
      CommonAlertsButtonGroups.YesNo);
      
    if (result == CommonAlertsButtons.Yes)
      this.districtService.deleteById(id)
        .subscribe(() => {
          this.loadAllDistrictDto();
        }, err => { });
  }
}
