import { Component, OnInit } from '@angular/core';
import { ProfileService, Profile } from '@abp/ng.core'
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {

  private formGroup: FormGroup;

  constructor(
    private profileService: ProfileService) { 
    this.configureFormGroup();
  }

  ngOnInit() 
  { }

  private saveChanges() {
    if (this.formGroup.invalid) return;
    var dto = this.getDto();

    this.profileService.changePassword(dto)
      .subscribe(dto => {
        console.log('password changed');
      }, err=>{});
  }

  private getDto() {
    var dto: Profile.ChangePasswordRequest = {
      currentPassword: this.formGroup.get('currentPassword').value,
      newPassword: this.formGroup.get('newPassword').value
    };

    return dto;
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      currentPassword: new FormControl('', Validators.compose([                
        Validators.required,        
      ])),
      newPassword: new FormControl('', Validators.compose([
        Validators.required
      ])),
      confirmPassword: new FormControl('',Validators.compose([
        Validators.required
      ])),
    }, { validators: validator });
  }
}

const validator: ValidatorFn = (formGroup: FormGroup): ValidationErrors | null => {
  var newPw = formGroup.get('newPassword');
  var confirmPw = formGroup.get('confirmPassword');

  if (formGroup && newPw && confirmPw) {
    if (newPw.value != '' && newPw.value != confirmPw.value) {
      confirmPw.setErrors({ invalidConfirmPasswordField: true });
      return { invalidConfirmPasswordField: true };
    }
    else if (newPw.value == '' && confirmPw.value != '') {
      confirmPw.setErrors({ invalidConfirmPasswordField: true });
      return { invalidConfirmPasswordField: true };
    }
  }
}
