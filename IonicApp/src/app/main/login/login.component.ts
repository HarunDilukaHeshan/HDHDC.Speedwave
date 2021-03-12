import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { CommonAlerts, CommonAlertsButtonGroups } from 'src/app/shared/dependencies/common-alerts.injectable';
import { ConfigStateService } from '@abp/ng.core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  private formGroup: FormGroup;

  constructor(
    private navCtrl: NavController,
    private configStateService: ConfigStateService,
    private commonAlerts: CommonAlerts,
    private oAuthService: OAuthService) { 
    this.configureFormGroup();    
  }

  ngOnInit() {     
    this.init();
  }

  private async init() {
    
    await this.oAuthService.loadDiscoveryDocument();    
  }

  private async loginAsync() {
    if (this.formGroup.invalid) {
      await this.commonAlerts.errorAlert('', 'Please enter valid username and password', '', CommonAlertsButtonGroups.Ok);            
      return;
    }
    
    var username = this.formGroup.get('username').value;
    var password = this.formGroup.get('password').value;

    await this.oAuthService.fetchTokenUsingPasswordFlow(username, password);
    
    this.configStateService.dispatchGetAppConfiguration()
        .subscribe((dt) => {
          console.log(dt);
          this.navCtrl.navigateRoot('');
    });
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      username: new FormControl('', Validators.compose([
        Validators.required
      ])),
      password: new FormControl('', Validators.compose([
        Validators.required
      ]))
    });
  }
}
