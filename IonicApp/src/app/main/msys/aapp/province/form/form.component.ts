import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { ProvinceService } from 'src/app/proxy/controllers';
import { ProvinceDto } from 'src/app/proxy/dtos';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;

  private isAwaiting: boolean = false;    

  constructor(
    private navController: NavController,
    private provinceService: ProvinceService) 
  { }

  ngOnInit() 
  {
    this.configureFormGroup();
  }

  private save()
  {
    if (this.isAwaiting || this.formGroup.invalid) return;

    var dto = this.getCreateDto();
    this.provinceService.createByDto(dto)
      .subscribe(data => {
        this.navController.back();
      }, err => { });
  }

  private getCreateDto()
  {
    var dto = {} as ProvinceDto;
    dto.id = this.formGroup.get('provinceName').value;
    return dto;
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      provinceName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ]))
    });
  }
}
