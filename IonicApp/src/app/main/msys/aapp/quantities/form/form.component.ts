import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { QuantityDto, UnitDto } from 'src/app/proxy/dtos';
import { QuantityService, UnitService } from 'src/app/proxy/controllers';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;

  private awaiting: boolean = false;

  private unitArr: Array<UnitDto> = [];

  constructor(
    private quantityService: QuantityService, 
    private unitService: UnitService,
    private navController: NavController) {
      this.configureFormGroup();
      this.loadAllUnit();
  }

  ngOnInit() {}

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      quantity: new FormControl(1, Validators.compose([
        Validators.required,
        Validators.min(0.01)
      ])),
      unit: new FormControl(0, Validators.compose([
        Validators.required        
      ]))      
    });
  }

  private loadAllUnit()
  {
    this.unitService.getAll()
      .subscribe(data=>{
        this.unitArr = data;
      }, err=>{});
  }

  private createQuantity() {
    if (!this.awaiting && this.formGroup.valid) {
      this.awaiting = true;
      var dto = this.getQuantityDto();
      this.quantityService.createByDto(dto)
        .subscribe(data => {
          this.awaiting = false;
          this.navController.back();
        }, err => {
          this.awaiting = false;
         // this.navController.back();
        });
    }
  }

  private getQuantityDto()
  {
    var dto = {} as QuantityDto;
    dto.quantity = this.formGroup.get('quantity').value as number;
    dto.unitID = this.formGroup.get('unit').value as string;
    return dto;
  }
}
