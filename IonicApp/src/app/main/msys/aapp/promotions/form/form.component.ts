import { invalid } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { PromotionService } from 'src/app/proxy/controllers';
import { PromotionCreateDto, PromotionDto, PromotionUpdateDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private isUpdatingForm: boolean = false;

  private formGroup: FormGroup;

  private promotionDto: PromotionDto;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private promotionService: PromotionService) 
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.init();    
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var promotionId = params['promotionId'];
        if (promotionId && !isNaN(promotionId)) {
          this.isUpdatingForm = true;
          this.loadPromotionDto(promotionId);
        }
      });
  }

  private loadPromotionDto(id: number) {
    this.promotionService.getById(id)
      .subscribe(dto => {
        this.promotionDto = dto;
        this.setupFormGroup(dto);
      }, err => {
        this.navController.back();
        throw Error('Unexpected error occured');
      });
  }

  private setupFormGroup(dto: PromotionDto)
  {
    this.formGroup.get('isOneTime').setValue(dto.isOneTime);
    this.formGroup.get('numberOfTimes').setValue(dto.noOfTimes);
    this.formGroup.get('expireDate').setValue(dto.expireDate);

    this.formGroup.get('isOneTime').disable();
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      isOneTime: new FormControl(true, Validators.compose([
        Validators.required
      ])),
      numberOfTimes: new FormControl(1, Validators.compose([
        Validators.required,
        Validators.min(1)
      ])),
      expireDate: new FormControl('', Validators.compose([
        Validators.required,
        expireDateValidator
      ]))
    }, { validators: promotionCrossValidator });
  }

  private save() {
    if (this.formGroup.invalid || this.isWaiting) return;

    if (this.isUpdatingForm)
      this.update();
    else
      this.create();
  }

  private create() {
    var dto = this.getCreateDto();
    console.log(dto);
    this.promotionService.createByDto(dto)
      .subscribe(data => {
        this.isUpdatingForm = true;
        this.loadPromotionDto(data.id);
      }, err => { })
  }

  private update() {
    var dto = this.getUpdateDto();

    this.promotionService.updateByIdAndDto(this.promotionDto.id, dto)
      .subscribe(data => {
        alert('updated');
      }, err => {

      });
  }

  private getCreateDto() {
    var dto = {} as PromotionCreateDto;

    dto.isOneTime = this.formGroup.get('isOneTime').value as boolean;
    dto.noOfTimes = this.formGroup.get('numberOfTimes').value as number;
    dto.expireDate = this.formGroup.get('expireDate').value as string;

    return dto;
  }

  private getUpdateDto() {
    var dto = {} as PromotionUpdateDto;
    dto.noOfTimes = this.formGroup.get('numberOfTimes').value as number;
    dto.expireDate = this.formGroup.get('expireDate').value as string;

    return dto;
  }
}

const promotionCrossValidator: ValidatorFn | null = 
  (control: FormGroup) => {
    var isOneTime = control.get('isOneTime')?.value as boolean;
    var noOfTimes = control.get('numberOfTimes')?.value as number;

    if (isOneTime != null && noOfTimes != null) {
      if (isOneTime && noOfTimes > 1) {
        control.get('numberOfTimes').setErrors({ 'Invalid': true });
        return { 'NumberOfTimesMustBeGreaterThan2': true };
      }

      if (!isOneTime && noOfTimes < 2) {
        control.get('numberOfTimes').setErrors({ 'Invalid': true });
        return { 'NumberOfTimesMustBeEqualTo1': true }
      }
    }
};

const expireDateValidator: ValidatorFn | null = 
  (control: FormControl) => {
    if (control) {
      var expDate = new Date(control.value);
      var now = new Date(Date.now());    

      if (isNaN(expDate.getTime())) return;

      var eY = expDate.getFullYear();
      var eM = expDate.getMonth();
      var eD = expDate.getDate();
      var nY = now.getFullYear();
      var nM = now.getMonth();
      var nD = now.getDate();

      if (eY > nY || 
        (eY == nY && eM > nM) || 
        (eY == nY && eM == nM && eD > nD)) return;
        
      return { 'InvalidExpDate': true };
    }
};