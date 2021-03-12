import { MapRestriction } from '@agm/core/services/google-maps-types';
import { Component, OnInit } from '@angular/core';
import { CityService } from 'src/app/proxy/controllers';
import { CityDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss'],
})
export class CityComponent implements OnInit {
  
  private cityDtoArr: Array<CityDto> = [];

  constructor(
    private commonAlerts: CommonAlerts,
    private cityService: CityService) 
  { }

  ngOnInit() {
    this.loadAllCityDto();
  }

  private loadAllCityDto()
  {
    this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId()
      .subscribe(data => {
        this.cityDtoArr = data;
        console.log(data);
      }, err => {});
  }

  private async delete(id: number)
  {
    var alert = await this.commonAlerts.warningAlert(
      '', 
      'Warning', 
      'Do you want to delete this item?', 
      CommonAlertsButtonGroups.YesNo);

    if (alert == CommonAlertsButtons.No) return;

    this.cityService.deleteById(id)
      .subscribe(() => {
        this.loadAllCityDto();
      }, err => {
      });
  }
}
