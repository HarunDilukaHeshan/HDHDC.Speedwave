import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IonSlides, NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { CustomerAccountService } from 'src/app/proxy/account-controllers';
import { UserService } from 'src/app/proxy/controllers';
import { UserCreateDto } from 'src/app/proxy/dtos';
import { UsernameAvailabilityValidator } from 'src/app/validators/usernameAvailability.validator';
import { OAuthService } from 'angular-oauth2-oidc';
import { ConfigStateService } from '@abp/ng.core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {

  private formGroup: FormGroup;
  private isWaiting: boolean = false;

  @ViewChild('slideComp')
  private slidesPages: IonSlides;

  constructor(
    private configStateService: ConfigStateService,
    private userService: UserService,
    private navCtrl: NavController,
    private oAuthService: OAuthService,
    private customerAccountService: CustomerAccountService) 
  { 
    this.configureFormGroup();
  }

  ngOnInit() {           
    this.login('customer01', 'Customer/01').then(() => {
      var dd = this.oAuthService.getAccessToken();
      console.log(dd);
      //this.configStateService.dispatchGetAppConfiguration()
       // .subscribe();
    });    
  }

  private slidesCtrl = {
    onSlideChange: async () => {
      this.slidesCtrl.isBeginning = await this.slidesPages.isBeginning();
      this.slidesCtrl.isEnd = await this.slidesPages.isEnd();
    },
    isBeginning: true,
    isEnd: false
  };

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      name: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      surname: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.email
      ])),
      phoneNo: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ])),
      username: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z][a-zA-Z0-9]/)        
      ]), UsernameAvailabilityValidator.getAsyncValidator(this.userService)),
      password: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z0-9]/)
      ]))
    });
  }

  private save() {
    if (this.isWaiting || this.formGroup.invalid) return;

    this.isWaiting = true;
    var dto = this.getCreateDto();
    this.customerAccountService.createByDto(dto)
      .subscribe(dto => {
        this.isWaiting = false;
      }, err => {
        this.isWaiting = false;
      }, () => {
        this.isWaiting = false;
      });
  }

  private getCreateDto() {
    var dto = {} as UserCreateDto;

    dto.name = this.formGroup.get('name').value as string;
    dto.surname = this.formGroup.get('surname').value as string;
    dto.phoneNumber = this.formGroup.get('phoneNo').value as string;
    dto.emailAddress = this.formGroup.get('email').value as string;
    dto.userName = this.formGroup.get('username').value as string;
    dto.password = this.formGroup.get('password').value as string;

    return dto;
  }

  private async login(username: string, password: string) {
    await this.oAuthService.loadDiscoveryDocument();
    var dd = await this.oAuthService.fetchTokenUsingPasswordFlow(username, password);    
  }
}
