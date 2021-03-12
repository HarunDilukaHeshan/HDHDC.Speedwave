import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { PasswordResetPinConstraints } from 'src/app/constraints/password-reset-pin-constriants';
import { UserService } from 'src/app/proxy/controllers';
import { PasswordResetPinRequestService } from 'src/app/shared/services/password-reset-pin-request.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
  providers: [PasswordResetPinRequestService]
})
export class ForgotPasswordComponent implements OnInit {

  private resendBtnText: string = "Send";
  private canRequest: boolean = false;
  private count: number = 0;
  private countDownTimerId: number;

  private formGroup: FormGroup;

  constructor(
    private navCtrl: NavController,
    private resetPinRequestService: PasswordResetPinRequestService,
    private userService: UserService) {       
      this.configureFormGroup();      
      this.init();
  }

  ngOnInit() {
    
  }

  private init() {
    var userName = this.formGroup.get('userName');
    this.resetPinRequestService.canRequest()
      .subscribe(canRequest => {
        this.canRequest = canRequest;
        if (canRequest) {
          userName.enable();
        }
        else {
          this.resetPinRequestService.getCurrentRequest()
            .subscribe(request => {
              userName.disable();
              userName.setValue(request.userName);
              this.formGroup.get('pinNo').enable();
              this.resendBtnText = 'Resend';
              this.startCountDown();
            });
        }
      });
  }

  private startCountDown() {
    this.resetPinRequestService.getCurrentRequest()
      .subscribe(request => {
        var t = new Date(request.requestedTime);
        this.countDownTimerId = window.setInterval(() => {
          var intvl = t.getTime() + (PasswordResetPinConstraints.interval * 60000) - (new Date(Date.now())).getTime();
          this.count = parseInt((intvl / 1000).toString());
          if (intvl < 1) {
            window.clearInterval(this.countDownTimerId);
            this.resetPinRequestService.canRequest()
              .subscribe(canRequest => { 
                this.canRequest = canRequest; 
                this.formGroup.get('userName').enable();
              });
          }
        }, 1000);
      });
  }

  private sendRequest() {
    var userName = this.formGroup.get('userName');
    if (!userName || userName.invalid) return;

    this.resetPinRequestService.sendRequest(userName.value)      
      .subscribe(()=>{
        this.init();
      }, err => {
        this.navCtrl.navigateForward('exceptions/something-went-wrong');
      });
  }

  private resetPassword() {
    if (this.formGroup.invalid) return;

    this.userService.resetPasswordByDto({
      userName: this.formGroup.get('userName').value,
      passwordResetToken: this.formGroup.get('pinNo').value,
      newPassword: this.formGroup.get('newPasswordFG.newPassword').value
    })
    .subscribe(() => {

    }, err => {

    });
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      userName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      pinNo: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.passwordResetTokenLength),
        Validators.maxLength(ModelConstraints.passwordResetTokenLength),
        Validators.pattern(/\d{6}/)
      ])), 
      newPasswordFG: new FormGroup({
        newPassword: new FormControl('', Validators.compose([
          Validators.required,
          Validators.minLength(ModelConstraints.passwordMinLength),
          Validators.maxLength(ModelConstraints.passwordMaxLength)
        ])),
        confirmPassword: new FormControl('', Validators.compose([
          Validators.required,
          Validators.minLength(ModelConstraints.passwordMinLength),
          Validators.maxLength(ModelConstraints.passwordMaxLength)
        ]))
      }, { validators: passwordValidator })
    }, { validators: formValidator });
  }

}

const formValidator: ValidatorFn = (fg: FormGroup): ValidationErrors | null  => {
  var userNameFC = fg.get('userName');
  var pinNoFC = fg.get('pinNo');
  var newPasswordFG = fg.get('newPasswordFG');
  var newPasswordFC = fg.get('newPasswordFG.newPassword');
  var confirmPasswordFC = fg.get('newPasswordFG.confirmPassword');

  if (!(userNameFC && pinNoFC && newPasswordFC && confirmPasswordFC)) return null;
  if (userNameFC.valid || userNameFC.disabled) { 
    if (pinNoFC.disabled) pinNoFC.enable();
    if (pinNoFC.valid && newPasswordFG.disabled) 
      newPasswordFG.enable();
    else if (pinNoFC.invalid && newPasswordFG.enabled) 
      newPasswordFG.disable();    
  }
  else if (pinNoFC.enabled) { 
    if (pinNoFC.value != '') pinNoFC.setValue('');
    pinNoFC.disable();  
    newPasswordFG.disable();   
  }
};

const passwordValidator: ValidatorFn = (fg: FormGroup): ValidationErrors | null => {
  var newPasswordFC = fg.get('newPassword');
  var confirmPasswordFC = fg.get('confirmPassword');

  if (!(newPasswordFC && confirmPasswordFC)) return null;

  if (newPasswordFC.value != confirmPasswordFC.value)
    confirmPasswordFC.setErrors({ mistake: true });
};