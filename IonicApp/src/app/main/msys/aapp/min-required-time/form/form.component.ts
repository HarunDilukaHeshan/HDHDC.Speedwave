import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MinRequiredTimeService } from 'src/app/proxy/controllers';
import { NavController, IonDatetime, PickerController } from '@ionic/angular';
import { MinRequiredTimeDto } from 'src/app/proxy/dtos';
import { PickerOptions, PickerColumn, PickerButton, PickerColumnOption } from '@ionic/core';
import { async } from '@angular/core/testing';
import { PickerSelection } from 'src/app/shared/components/datetime-span-picker/datetime-span-picker.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;
  private awaiting: boolean = false;  

  constructor(
    private mrtService: MinRequiredTimeService, 
    private pickerController: PickerController,
    private navController: NavController) 
  { 
    this.configureFormGroup();
  }

  ngOnInit() {}

  private createMrt() {
    if (this.awaiting) return;

    this.awaiting = true;
    var dto = this.getDto();
    this.mrtService.createByDto(dto)
      .subscribe(data => {
        this.awaiting = false;
        this.navController.back();
      }, err => {
        this.awaiting = false;
      });
  }

  private onChange(ev: CustomEvent<PickerSelection>)
  {
    console.log(ev);
    var pickerS = ev.detail;
    var timeStr = pickerS.days.value + '.' + pickerS.hours.value + ':' + pickerS.minutes.value + ':00';
    this.formGroup.get('mrt').setValue(timeStr);
  }

  private getDto()
  {
    var dto = {} as MinRequiredTimeDto;
    dto.minRequiredTime = this.formGroup.get('mrt').value as string;
    return dto;
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      mrt: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(/\d\d?.\d\d?:\d\d?:\d\d?/)
      ]))
    });
  }
}
