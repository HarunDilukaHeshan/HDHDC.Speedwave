import { Component, OnInit } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';
import { UserService } from 'src/app/proxy/controllers';
import { AppUserDto } from 'src/app/proxy/dtos';
import { NavController } from '@ionic/angular';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { ProfileService, Profile } from '@abp/ng.core'


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {

  private dto: AppUserDto;
  private formGroup: FormGroup;

  constructor(
    private profileService: ProfileService,
    private navCtrl: NavController,
    private userService: UserService,
    private configStateService: ConfigStateService) { 
    this.configureFormGroup();
  }

  ngOnInit() { 
    this.loadCurrentUserDto();
  }

  private loadCurrentUserDto() {
    this.userService.get()
      .subscribe(dto => {
        this.dto = dto;
        this.setupValues(dto);
      }, err => {
        this.navCtrl.navigateForward('/exceptions/something-went-wrong');
      });
  }

  private updateCurrentUser() {
    if (this.formGroup.invalid) return;

    var dto = this.getUserUpdateDto();
    this.profileService.update(dto)
      .subscribe(dto => {
        console.log(dto);
      });
  }

  private getUserUpdateDto() {
    var dto: Profile.Response = { 
      userName: this.formGroup.get('username').value,
      email: this.formGroup.get('email').value,
      name: this.formGroup.get('name').value,
      surname: this.formGroup.get('surname').value,
      phoneNumber: this.formGroup.get('phoneNo').value
    };

    return dto;
  }

  private setupValues(dto: AppUserDto) {
    if(!this.formGroup) return;

    this.formGroup.get('username').setValue(dto.userName);
    this.formGroup.get('name').setValue(dto.name);
    this.formGroup.get('surname').setValue(dto.surname);
    this.formGroup.get('email').setValue(dto.email);
    this.formGroup.get('phoneNo').setValue(dto.phoneNumber);    
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      username: new FormControl(''),
      name: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      surname:  new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      email:  new FormControl('', Validators.compose([
        Validators.required,
        Validators.email        
      ])),
      phoneNo:  new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ]))
    });
  }
}
