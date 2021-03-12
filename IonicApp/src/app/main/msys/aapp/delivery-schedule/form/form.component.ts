import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { DeliveryScheduleService } from 'src/app/proxy/controllers';
import { DeliveryScheduleCreateUpdateDto, DeliveryScheduleDto } from 'src/app/proxy/dtos';
import { PickerSelection } from 'src/app/shared/components/datetime-span-picker/datetime-span-picker.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private isUpdatingForm: boolean = false;

  private formGroup: FormGroup;

  private deliveryScheduleDto: DeliveryScheduleDto;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private deliveryScheduleService: DeliveryScheduleService)
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['deliveryScheduleId'];
        if (id && !isNaN(id)) {
          this.isUpdatingForm = true;
          this.loadDeliveryScheduleDto(id);
        }
      });
  }

  private loadDeliveryScheduleDto(id: number) {
    this.deliveryScheduleService.getById(id)
      .subscribe(dto => {
        this.deliveryScheduleDto = dto;
        this.setupFormGroup(dto);
      }, err => {
        this.navController.back();
      });
  }

  private setupFormGroup(dto: DeliveryScheduleDto)
  {
    this.formGroup.get('dsName').setValue(dto.deliveryScheduleName);
    this.formGroup.get('dsTimePeriod').setValue(dto.timePeriod);
    this.formGroup.get('dsCostIncreasePer').setValue(dto.costIncreasePercentage);
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      dsName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      dsTimePeriod: new FormControl('', Validators.compose([
        Validators.required
      ])),
      dsCostIncreasePer: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(0)
      ])),
    });
  }

  private save() {
    if (this.formGroup.invalid && this.isWaiting) return;

    if (this.isUpdatingForm)
      this.update();
    else
      this.create();
  }

  private create() {
    var dto = this.getDto();
    this.isWaiting = true;

    this.deliveryScheduleService.createByDto(dto)
      .subscribe(data => {
        this.isWaiting = false;
        this.isUpdatingForm = true;
        this.loadDeliveryScheduleDto(data.id);
      }, err => { 
        this.isWaiting = false;
      });
  }

  private update() {
    var dto = this.getDto();
    this.isWaiting = true;

    this.deliveryScheduleService.updateByIdAndDto(this.deliveryScheduleDto.id, dto)
      .subscribe(data => {
        this.isWaiting = false;
      }, err => {
        this.isWaiting = false;
      });
  }

  private getDto() {
    var dto = {} as DeliveryScheduleCreateUpdateDto;

    dto.deliveryScheduleName = this.formGroup.get('dsName').value as string;
    dto.timePeriod = this.parseDateTimeSpan(this.formGroup.get('dsTimePeriod').value);
    dto.costIncreasePercentage = this.formGroup.get('dsCostIncreasePer').value as number;
    console.log(this.formGroup.get('dsTimePeriod').value);
    return dto;
  }

  private parseDateTimeSpan(timespan: PickerSelection)
  {
    if (!timespan) return null;
    return timespan.days.value + '.' + timespan.hours.value + ':' + timespan.minutes.value;
  }

  private change(ev: CustomEvent)
  {
    console.log(ev.detail);
  }
}
