import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DoCheck, OnChanges, OnInit } from '@angular/core';
import { CityService, DistrictService, RiderService } from 'src/app/proxy/controllers';
import { CityDto, DistrictDto, RiderDto, RiderUpdateDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';
import { OAuthService } from 'angular-oauth2-oidc';
import { GeoCoordsParser } from 'src/app/shared/dependencies/geo-coords-parser';

@Component({
  selector: 'app-rider-info',
  templateUrl: './rider-info.component.html',
  styleUrls: ['./rider-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RiderInfoComponent implements OnInit, DoCheck {

  private riderDto: RiderDto;
  private districtDtoArr: Array<DistrictDto> = [];
  private cityDtoArr: Array<CityDto> = [];
  private cityWithinRadiusDtoArr: Array<CityDto> = [];  

  private initCoords: GeoCoords = null;
  private markers: Array<MapMarker> = [];

  private selectedDistrictId: string = '';
  private selectedCityId: number = 0;
  private cityIdsArr: Array<number> = [];
  private newGeoL: GeoCoords = null;
  private isWaiting: boolean = false;

  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private oAuthService: OAuthService,
    private riderService: RiderService,
    private districtService: DistrictService,
    private cityService: CityService) {
  }

  ngOnInit() { 
    this.login();
    this.init();    
  }

  ngDoCheck() { }

  // delete in the production
  private async login() {
    await this.oAuthService.loadDiscoveryDocument();
    var token = await this.oAuthService.fetchTokenUsingPasswordFlow('riderThree', 'riderThree/03');
  }

  private async init() {
    var districtTask = this.loadAllDistrictDto();
    var riderTask = this.loadCurrentRider();
    var riderCTask = this.loadAllRiderCoverage();      

    this.riderDto = await riderTask;
    var cityDtoArr = await riderCTask;
    this.districtDtoArr = await districtTask;

    this.cityIdsArr = cityDtoArr.map(val => val.id);
    var cityDtoArrTask = this.loadAllCityDto(this.riderDto.cityDto.districtID);
    var cityWithinRTask = this.loadAllCityWithinRadius(this.riderDto.cityID);

    this.cityDtoArr = await cityDtoArrTask;
    this.cityWithinRadiusDtoArr = await cityWithinRTask;

    this.selectedDistrictId = this.riderDto.cityDto.districtID;
    this.selectedCityId = this.riderDto.cityID;
    
    this.changeDetectorRef.detectChanges();
  }

  private loadCurrentRider() {
    return this.riderService.getCurrentRider().toPromise();
  }

  private loadAllRiderCoverage() {
    return this.riderService.getRiderCoverage().toPromise();
  }

  private loadAllDistrictDto() {
    return this.districtService.getAll().toPromise();
  }

  private loadAllCityDto(districtId: string) {    
    return this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId(
      null, null, null, null, districtId).toPromise();
  }

  private loadAllCityWithinRadius(cityId: number) {
    return this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId(
      null, null, cityId, null, null).toPromise();
  }

  private onDistrictChange(ev: CustomEvent<{ value: string }>) {    
    this.loadAllCityDto(ev.detail.value).then(val => {
      this.selectedCityId = (this.riderDto && this.riderDto.cityDto.districtID == ev.detail.value)? this.riderDto.cityID : 0;
      this.cityDtoArr = val;
      this.changeDetectorRef.markForCheck();
      //this.changeDetectorRef.detectChanges();
    })                
  }

  private async onCityChange(ev: CustomEvent<{ value: number }>) {
    var cityDto = this.cityDtoArr.find(val => val.id == ev.detail.value);

    if (!cityDto) { 
      this.cityWithinRadiusDtoArr = [];
      this.cityIdsArr = [];
      this.initCoords = null;
      this.markers = [];
      return; 
    }
    
    if (this.riderDto.cityID == ev.detail.value)
      this.cityIdsArr = (await this.loadAllRiderCoverage()).map(val => val.id);

    this.cityWithinRadiusDtoArr = await this.loadAllCityWithinRadius(ev.detail.value);    
    
    var geoC = GeoCoordsParser.parse( (this.newGeoL)? cityDto.geolocation : this.riderDto.geolocation);
    this.initCoords = geoC;
    this.markers = [new MapMarker(geoC)];
    this.newGeoL = geoC;
    this.changeDetectorRef.detectChanges();
    console.log('Triggered');    
  }

  private onCoverageChange(ev: CustomEvent<{ value: [] }>) {
    this.cityIdsArr = ev.detail.value;
  }

  private onMapClick(ev: MapClickEvent) {
    this.newGeoL = new GeoCoords(ev.coords.lat, ev.coords.lng);
    this.markers = [new MapMarker(this.newGeoL)];
  }

  private getCityName(cityId: number) {
    var i = this.cityWithinRadiusDtoArr.find(val => val.id == cityId);
    return (i)? i.cityName : '';
  }

  private async save() {
    this.isWaiting = true;
    if (this.selectedCityId == this.riderDto.cityID) {
      // city has not changed
      if (this.newGeoL) {
        // geolocation has changed
        await this.updateRiderDto();
      }
    }
    else {          
        await this.updateRiderDto();      
    }

    // update rider coverage
    await this.updateRiderCoverage();
    this.isWaiting = false;
    this.changeDetectorRef.detectChanges();
  }

  private updateRiderDto() {
    var dto = this.getRiderUpdateDto();
    return this.riderService.updateByIdAndDto(this.riderDto.id, dto).toPromise();
  }

  private updateRiderCoverage() {
    return this.riderService.updateRiderCoverageByCityIds(this.cityIdsArr).toPromise();      
  }

  private getRiderUpdateDto() {
    var dto = { } as RiderUpdateDto;

    dto.geolocation = (this.newGeoL)? this.newGeoL.lat + ':' + this.newGeoL.lng : this.riderDto.geolocation;
    dto.cityID = this.selectedCityId;

    return dto;
  }
}
