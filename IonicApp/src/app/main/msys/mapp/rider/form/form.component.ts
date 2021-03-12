import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IonSlides, NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { CityService, RiderService, UserService } from 'src/app/proxy/controllers';
import { CityDto, RiderCreateDto, UserCreateDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';
import { UsernameAvailabilityValidator } from 'src/app/validators/usernameAvailability.validator';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private formGroup: FormGroup;

  private initCoords: GeoCoords = null;

  private markers: Array<MapMarker> = [];

  private cityDtoArr: Array<CityDto> = [];

  @ViewChild('slideComp')
  private slidesPages: IonSlides;  

  constructor(
    private navController: NavController,
    private userService: UserService,
    private cityService: CityService, 
    private riderService: RiderService)
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.loadAllCityDto();
  }

  private slidesCtrl = {
    onSlideChange: async () => {
      this.slidesCtrl.isBeginning = await this.slidesPages.isBeginning();
      this.slidesCtrl.isEnd = await this.slidesPages.isEnd();
    },
    isBeginning: true,
    isEnd: false
  };

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      name: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      surname: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.email
      ])),
      phoneNo: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ])),
      city: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(1)
      ])),
      geolocation: new FormControl('', Validators.compose([
        Validators.required
      ])),
      username: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z][a-zA-Z0-9]/)        
      ]), UsernameAvailabilityValidator.getAsyncValidator(this.userService)),
      password: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z0-9]/)
      ]))
    });
  }

  private loadAllCityDto() {
    this.cityService.searchBySkipCountAndMaxResultCountAndCityIdAndKeywordAndDistrictId()
      .subscribe(dtoArr => {
        this.cityDtoArr = dtoArr;
      }, err => {});
  }

  private onMapClick(ev: MapClickEvent) {
    var geol = new GeoCoords(ev.coords.lat, ev.coords.lng);    
    var mapMarker = new MapMarker(geol);
    this.markers = [mapMarker];
    this.formGroup.get('geolocation').setValue(mapMarker.coords.lat + ':' + mapMarker.coords.lng);
  }

  private onCityChange(ev: CustomEvent) {
    var val = ev.detail.value;
    var cityDto = this.cityDtoArr.find(dto => dto.id == val);
    var geolocationAr = cityDto.geolocation.split(':');
    var geo = {lat: parseFloat(geolocationAr[0]), lng: parseFloat(geolocationAr[1]) };
    var geol = new GeoCoords(parseFloat(geo.lat.toFixed(10)), parseFloat(geo.lng.toFixed(10)));

    this.initCoords = geol;
    this.markers = [new MapMarker(geol)];
    this.formGroup.get('geolocation').setValue(geol.lat + ':' + geol.lng);
  }

  private save() {
    if (this.isWaiting || this.formGroup.invalid) return;

    var dto = this.getDto();
    this.riderService.createByDto(dto)
      .subscribe(dto => {
        alert('Created');                
        this.navController.back();
      }, err => {});
  }

  private getDto() {
    var dto = {} as RiderCreateDto;
    var cDto = {} as UserCreateDto;

    dto.userCreateDto = cDto;
    
    dto.cityID = this.formGroup.get('city').value as number;    
    dto.geolocation = this.formGroup.get('geolocation').value as string;

    cDto.name = this.formGroup.get('name').value as string;
    cDto.surname = this.formGroup.get('surname').value as string;
    cDto.emailAddress = this.formGroup.get('email').value as string;
    cDto.phoneNumber = this.formGroup.get('phoneNo').value as string;
    cDto.userName = this.formGroup.get('username').value as string;
    cDto.password = this.formGroup.get('password').value as string;

    return dto;
  }
}
