import { ChangeDetectorRef, Component, DoCheck, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OAuthService } from 'angular-oauth2-oidc';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { CustomerAccountService } from 'src/app/proxy/account-controllers';
import { CategoryService, CityService, DistrictService } from 'src/app/proxy/controllers';
import { AddressCreateDto, AddressDto, AddressUpdateDto, CityDto, DistrictDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';
import { GeoCoordsParser } from 'src/app/shared/dependencies/geo-coords-parser';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isUpdating: boolean = false;
  private isWaiting: boolean = false;
  private formGroup: FormGroup;
  private dto: AddressDto;
  private cityDto: CityDto;
  private cityDtoArr: Array<CityDto> = [];
  private districtDtoArr: Array<DistrictDto> = [];  
  private initCoords: GeoCoords = null;
  private markers: Array<MapMarker> = [];

  constructor(
    private cd: ChangeDetectorRef,
    private navCtrl: NavController,
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService,
    private districtService: DistrictService,
    private cityService: CityService,
    private customerService: CustomerAccountService,
    private oAuthService: OAuthService) 
  { 
    
  }

  ngOnInit() {
    this.configureFormGroup();    
    this.init();       
  }

  private async init() {
    var params = await this.loadRouteParams();    
    var id = params['addressId'];

    await this.loadAllDistrictDto();    

    if (id && !isNaN(id)) {
      var dto = await this.loadAddressDto(id);
      var cityDto = await this.loadCityDto(dto.cityID);

      this.cityDtoArr = await this.loadAllCityDto(cityDto.districtID);         

      this.isUpdating = true;
      this.setFormGroupValues(dto);                         
      this.formGroup.get('district').setValue(cityDto.districtID);
      this.formGroup.get('city').setValue(cityDto.id);            
      this.cd.detectChanges();
    }  
  }

  private loadRouteParams() {
    return new Promise<Params>((resolve, reject) => {
      this.activatedRoute.params
        .subscribe(params => { resolve(params); }, err => { reject(err); }, () => { resolve(); });
    });
  }

  private loadAddressDto(id: number) {
    return new Promise<AddressDto>((resolve, reject) => {
      this.customerService.getAddressById(id)
        .subscribe(dto => {
          this.dto = dto;
          resolve(dto);
        }, err => {
          reject(err);
        }, () => {
          resolve();
        });
    });
  }

  private async loadAllDistrictDto() {
    return new Promise<DistrictDto[]>((resolve, reject) => {
      this.districtService.getAll()
        .subscribe(dtoArr => {
          this.districtDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => {
          reject(err);
        }, () => {
          resolve();
        });
    });
  }

  private async loadAllCityDto(districtId: string) {
    return new Promise<CityDto[]>((resolve, reject) => {
      this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId(null, null, null, null, districtId)
        .subscribe(dtoArr => {
          this.cityDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => {
          reject(err);
        }, () => {
          resolve();
        });
    });
  }

  private loadCityDto(id: number) {
    return new Promise<CityDto>((resolve, reject) => {
      this.cityService.getById(id)
        .subscribe(dto => {
          this.cityDto = dto;
          resolve(dto);
        }, err => {
          reject(err);
        }, () => {
          resolve();
        });
    });
  }

  private onMapClick(ev: MapClickEvent) {
    var mapMarker = new MapMarker(new GeoCoords(ev.coords.lat, ev.coords.lng));
    this.markers = [mapMarker];
    this.formGroup.get('geolocation').setValue(mapMarker.coords.lat + ':' + mapMarker.coords.lng);
  }

  private async onDistrictChange(ev: CustomEvent<{ value: string }>) {        
    await this.loadAllCityDto(ev.detail.value);   
  }

  private async onCityChange(ev: CustomEvent<{ value: number }>) {
    var dto = this.cityDtoArr.find(val => val.id == ev.detail.value);

    if (!dto || (this.cityDto && dto && this.cityDto.id == dto.id)) return;

    var coords = GeoCoordsParser.parse(dto.geolocation);
    if (coords == null) throw Error('invalid geocoords');

    this.formGroup.get('geolocation').setValue(dto.geolocation);
    this.initCoords = coords;
    this.markers = [new MapMarker(coords)];    
  }

  private getMarker(geolocation: string) {
    var marker: MapMarker;
    try {
      var geoCoords = geolocation.split(':');
      var lat = (geoCoords[0] as unknown) as number;
      var lng = (geoCoords[1] as unknown) as number;

      marker = new MapMarker(new GeoCoords(lat, lng));
    }
    catch (ex) { throw Error('Invalid geocoords'); }

    return marker;
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      address: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.addressMinLength),
        Validators.maxLength(ModelConstraints.addressMaxLength)        
      ])),
      note: new FormControl(null, Validators.compose([
        Validators.minLength(ModelConstraints.descriptionMinLength),
        Validators.maxLength(ModelConstraints.descriptionMaxLength)
      ])),
      district: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      geolocation: new FormControl('', Validators.compose([
        Validators.required        
      ]))
    });
  }

  private setFormGroupValues(dto: AddressDto) {
    this.formGroup.get('address').setValue(dto.addressLine);    
    this.formGroup.get('geolocation').setValue(dto.geolocation);
    this.formGroup.get('note').setValue(dto.note);

    var coords = GeoCoordsParser.parse(dto.geolocation);
    if (coords == null) throw Error('invalid geocoords');

    this.formGroup.get('geolocation').setValue(dto.geolocation);
    this.initCoords = coords;
    this.markers = [new MapMarker(coords)];
  }

  private async save() {
    if (this.isWaiting || this.formGroup.invalid) return;

    this.isWaiting = true;

    if (this.isUpdating)
      await this.update(this.dto.id);    
    else
      await this.create();

    this.isWaiting = false;
  }

  private create() {
    return new Promise<AddressDto>((resolve, reject) => {
      var dto = this.getCreateDto();
      this.customerService.createAddressByDto(dto)
        .subscribe(dto => { }, err => { }, () => { });
    });
  }

  private update(id: number) {
    return new Promise<AddressDto>((resolve, reject) => {
      var dto = this.getUpdateDto();
      this.customerService.updateAddressByIdAndDto(id, dto)
        .subscribe(dto => { }, err => { }, () => { });
    });
  }

  private getCreateDto() {
    var dto = {} as AddressCreateDto;

    dto.addressLine = this.formGroup.get('address').value as string;
    dto.cityID = this.formGroup.get('city').value as number;
    dto.geolocation = this.formGroup.get('geolocation').value as string;
    dto.note = this.formGroup.get('note').value as string;    

    return dto;
  }

  private getUpdateDto() {
    var dto = {} as AddressUpdateDto;

    dto.addressLine = this.formGroup.get('address').value as string;
    dto.geolocation = this.formGroup.get('geolocation').value as string;
    dto.note = this.formGroup.get('note').value as string;    

    return dto;
  }

  private async ll() {
    await this.oAuthService.loadDiscoveryDocument();
    await this.oAuthService.fetchTokenUsingPasswordFlow('customer01', 'Customer/01');
    this.categoryService.getAll()
      .subscribe();
  }
}
