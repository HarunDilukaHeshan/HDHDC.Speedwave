import { invalid } from '@angular/compiler/src/render3/view/util';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { CityService, DistrictService } from 'src/app/proxy/controllers';
import { CityDto, StoreBranchCreateDto, StoreBranchDto, StoreChainDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.scss'],
})
export class BasicInfoComponent implements OnInit {
  
  private formGroup: FormGroup;  

  private cityDtoArr: Array<CityDto> = [];

  private initCoords: GeoCoords = null;// = new GeoCoords(51.673858, 7.815982);

  private markers: Array<MapMarker> = [];

  @Input('storeChainDto')
  private storeChainDto: StoreChainDto;

  @Input('storeBranchDto')
  private storeBranchDto: StoreBranchDto;

  @Output('onStatusChange') 
  private onStatusChangeEv: EventEmitter<CustomEvent<FormStatusHolder>> = new EventEmitter<CustomEvent<FormStatusHolder>>();

  constructor(
    private cityService: CityService) {
    this.configureFormGroup();
  }

  ngOnChanges() {
    this.fillInTheForm();
  }

  ngOnInit() {    
    this.loadAllCityDto();    
  }

  private onMapClick(ev: MapClickEvent) {
    var mapMarker = new MapMarker(new GeoCoords(ev.coords.lat, ev.coords.lng));
    this.markers = [mapMarker];
    this.formGroup.get('geolocation').setValue(mapMarker.coords.lat + ':' + mapMarker.coords.lng);
  }

  private onCityChange(ev: CustomEvent) {
    var value = ev.detail.value;
    var dto = this.cityDtoArr.find(e => e.id == value);

    console.log('CityChange');
    if (dto == null || dto == undefined) return;

    var geol = (this.storeBranchDto && this.formGroup.get('geolocation').untouched)? this.storeBranchDto.geolocation : dto.geolocation;

    var coordsArr = geol.split(':');
    var lat: number = parseFloat(coordsArr[0]);
    var lng: number = parseFloat(coordsArr[1]);

    lat = parseFloat(lat.toFixed(10));
    lng = parseFloat(lng.toFixed(10));

    this.initCoords = new GeoCoords(lat, lng);
    this.markers = [new MapMarker(new GeoCoords(lat, lng))];
    this.onMapClick(new MapClickEvent(new GeoCoords(lat, lng)));
  }

  private loadAllCityDto() {
    this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId()
      .subscribe(dtoArr => {
        this.cityDtoArr = dtoArr;
        if (this.storeBranchDto)
          this.onCityChange(new CustomEvent('onCityChange', { detail: { value: this.storeBranchDto.cityID } }));
      }, err => {});
  }

  private onFormStatusChange(ev: string) {
    var dto = this.getDto();
    var fsHolder = new FormStatusHolder((ev.toLocaleLowerCase() === 'valid'), dto);    
    this.onStatusChangeEv.emit(new CustomEvent<FormStatusHolder>('onStatusChange', { detail: fsHolder }));
  }

  private fillInTheForm() {
    if (this.storeChainDto)
      this.formGroup.get('storeChainId').setValue(this.storeChainDto.id);

    if (!this.storeBranchDto) return;
    
    this.formGroup.get('cityId').setValue(this.storeBranchDto.cityID);
    this.formGroup.get('geolocation').setValue(this.storeBranchDto.geolocation);
    this.formGroup.get('contactNo01').setValue(this.storeBranchDto.contactNo01);
    this.formGroup.get('contactNo02').setValue(this.storeBranchDto.contactNo02);         
    this.formGroup.get('geolocation').markAsUntouched();   
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      storeChainId: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(1)
      ])),
      cityId: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(1)
      ])),
      geolocation: new FormControl('', Validators.compose([
        Validators.required,        
      ])),
      contactNo01: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ])),
      contactNo02: new FormControl('', Validators.compose([
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ]))
    }, { validators: ContactNosCrossValidator });

    this.formGroup.statusChanges
      .subscribe(data => {
        this.onFormStatusChange(data);
      });
  }

  private getDto() {
    var dto = {} as StoreBranchCreateDto;

    dto.storeChainID = this.formGroup.get('storeChainId').value;
    dto.cityID = this.formGroup.get('cityId').value;
    dto.geolocation = this.formGroup.get('geolocation').value;
    dto.contactNo01 = this.formGroup.get('contactNo01').value;
    dto.contactNo02 = this.formGroup.get('contactNo02').value;

    return dto;
  }
}

export class FormStatusHolder {

  private readonly _isValid: boolean;
  private readonly _dto: StoreBranchCreateDto;

  constructor(isValid: boolean, dto: StoreBranchCreateDto)
  {
    this._isValid = isValid;
    this._dto = dto;

    if (!isValid) this._dto = null;
  }

  get isValid() { return this._isValid; }
  get dto() {
    var dto = {} as StoreBranchCreateDto;
    dto.storeChainID = this._dto.storeChainID;
    dto.cityID = this._dto.cityID;
    dto.geolocation = this._dto.geolocation;
    dto.contactNo01 = this._dto.contactNo01;
    dto.contactNo02 = this._dto.contactNo02;
    return dto;
  }
}

const ContactNosCrossValidator: ValidatorFn | null = 
  (control: FormGroup) => {
    var no01 = control.get('contactNo01');
    var no02 = control.get('contactNo02');

    if (no01.valid && no02.valid) 
      if (no01.value === no02.value) 
      {
        no02.setErrors({ 'CannotBeSameAsTheContactNo01': true });
        return { 'TwoNumbersCannotBeSame': true }    
      }
};