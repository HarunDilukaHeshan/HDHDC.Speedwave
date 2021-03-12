import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { ProvinceService, DistrictService } from 'src/app/proxy/controllers';
import { ProvinceDto, DistrictCreateDto } from 'src/app/proxy/dtos';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;

  private isAwaiting: boolean = false;

  private provinceDtoList: Array<ProvinceDto> = [];

  constructor(
    private navController: NavController,
    private provinceService: ProvinceService, 
    private districtService: DistrictService) 
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.loadAllProvinceDto();
  }

  private loadAllProvinceDto()
  {
    this.provinceService.getAll()
      .subscribe(data => {
        this.provinceDtoList = data;
      }, err => { });
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      districtName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)        
      ])),
      province: new FormControl('', Validators.required)
    });
  }

  private save()
  {
    var dto = this.getCreateDto();
    this.districtService.createByDto(dto)
      .subscribe(data => {
        this.navController.back();
      }, err => {})
  }

  private getCreateDto()
  {
    var dto = {} as DistrictCreateDto;
    dto.id = this.formGroup.get('districtName').value;
    dto.provinceID = this.formGroup.get('province').value;
    return dto;
  }
}
