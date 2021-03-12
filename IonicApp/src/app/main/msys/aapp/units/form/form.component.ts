import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { UnitService } from 'src/app/proxy/controllers';
import { UnitDto } from 'src/app/proxy/dtos';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;
  private awaiting: boolean = false;

  constructor(
    private unitService: UnitService, 
    private navController: NavController) 
  { 
    this.configureFormGroup();
  }

  ngOnInit() {}

  private createUnit()
  {
    var dto = this.getUnitDto();    

    if (!this.awaiting && this.formGroup.valid) {
      this.awaiting = true;
      this.unitService.createByDto(dto)
        .subscribe(data => {
          this.awaiting = false;
          this.navController.back();
        }, err => {
          this.awaiting = false;
          this.navController.back();
        });
    }
  }

  private getUnitDto()
  {
    var dto = {} as UnitDto;

    dto.id = this.formGroup.get('unitName').value as string;
    dto.unitSymbol = this.formGroup.get('unitSymbol').value as string;
    
    return dto;
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      unitName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      unitSymbol: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ]))
    });
  }
}
