import { MapRestriction } from '@agm/core/services/google-maps-types';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { CityService, DistrictService } from 'src/app/proxy/controllers';
import { CityCreateDto, CityDto, CityUpdateDto, DistrictDto } from 'src/app/proxy/dtos';
import { GeoCoords, MapClickEvent, MapMarker } from 'src/app/shared/components/geolocation-reader/geolocation-reader.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isAwaiting: boolean = false;

  private isUpdatingForm: boolean = false;

  private formGroup: FormGroup;

  private cityDto: CityDto;

  private districtDtoArr: Array<DistrictDto> = [];

  private initCoords: GeoCoords = new GeoCoords(51.673858, 7.815982);

  private markers: Array<MapMarker> = []

  constructor(
    private districtService: DistrictService,
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private cityService: CityService)
  { }

  ngOnInit() 
  { 
    this.init();
  }

  private init()
  {
    this.configureFormGroup();
    this.loadAllDistrictDto();

    this.activatedRoute.params
      .subscribe(params => {
        var cityId = params['cityId'];
        if (cityId && !isNaN(cityId)) {
          this.isUpdatingForm = true;
          this.loadCityDto(cityId);          
        }
      });
  }

  private loadCityDto(cityId: number)
  {
    this.cityService.getById(cityId)
      .subscribe(dto => {
        this.cityDto = dto;
        this.fillInTheForm(dto);
      }, err => {
        this.navController.back();
      });
  }

  private loadAllDistrictDto()
  {
    this.districtService.getAll()
      .subscribe(data => {
        this.districtDtoArr = data;
      }, err => {});
  }

  private fillInTheForm(dto: CityDto)
  {
    var marker = this.getMarker(dto.geolocation);
    this.formGroup.get('cityName').setValue(dto.cityName);
    this.formGroup.get('districtId').setValue(dto.districtID);
    this.formGroup.get('geolocation').setValue(marker);
    this.markers = [marker];
    this.formGroup.updateValueAndValidity();
  }

  private getMarker(geolocation: string)
  {
    var marker: MapMarker;
    try {
      var geoCoords = geolocation.split(':');
      var lat = (geoCoords[0] as unknown) as number;
      var lng = (geoCoords[1] as unknown) as number;

      marker = new MapMarker(new GeoCoords(lat, lng));
    }
    catch (ex) {
      console.error('Invalid geocoords');
      throw Error('Invalid geocoords');
    }

    return marker;
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      cityName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      geolocation: new FormControl(null, Validators.compose([
        Validators.required
      ])),
      districtId: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(1)
      ]))
    });
  }  

  private onMapClick(ev: MapClickEvent)
  {
    var mapMarker = new MapMarker(new GeoCoords(ev.coords.lat, ev.coords.lng));
    this.markers = [mapMarker];
    this.formGroup.get('geolocation').setValue(mapMarker);
  }

  private save()
  {
    if (this.isAwaiting || this.formGroup.invalid) return;

    if (this.isUpdatingForm) 
      this.update();    
    else
      this.create();
  }

  private create()
  {
    var dto = this.getCreateDto();
    this.isAwaiting = true;

    this.cityService.createByDto(dto)
      .subscribe(data => {
        this.isAwaiting = false;
        this.isUpdatingForm = true;
        this.cityDto = data;
      }, err => {
        this.isAwaiting = false;
      });
  }

  private update()
  {
    var dto = this.getUpdateDto();
    this.isAwaiting = true;
    
    this.cityService.updateByIdAndDto(this.cityDto.id, dto)
      .subscribe(data => {
        this.isAwaiting = false;
      }, err => {
        this.isAwaiting = false;
      });
  }

  private getUpdateDto()
  {
    var dto = {} as CityUpdateDto;
    var marker: MapMarker = this.formGroup.get('geolocation').value;

    dto.cityName = this.formGroup.get('cityName').value;
    dto.geolocation = marker.coords.lat + ':' + marker.coords.lng;

    return dto;
  }

  private getCreateDto()
  {
    var dto = {} as CityCreateDto;
    var marker: MapMarker = this.formGroup.get('geolocation').value;

    dto.cityName = this.formGroup.get('cityName').value;
    dto.geolocation = marker.coords.lat + ':' + marker.coords.lng;
    dto.districtID = this.formGroup.get('districtId').value;

    return dto;
  }
}
