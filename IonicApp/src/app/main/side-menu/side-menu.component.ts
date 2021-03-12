import { Component, OnInit } from '@angular/core';
import { MenuController, NavController } from '@ionic/angular';
import { OAuthService } from 'angular-oauth2-oidc';
import { ConfigStateService } from '@abp/ng.core';
import { SideMenuController } from './side-menu-controller.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss'],
})
export class SideMenuComponent implements OnInit {

  private username: string = '';

  constructor(
    private menuCtrl: SideMenuController,
    private configStateService: ConfigStateService,
    private navCtrl: NavController,
    private oAuthService: OAuthService) 
  { }

  ngOnInit() 
  { 
    this.username = this.configStateService.getDeep('currentUser.userName');
  }

  private async logout() {
    this.oAuthService.logOut();
    await this.close();
    await this.navCtrl.navigateRoot('login');    
  }

  private async navToProfile() {
    await this.close();
    await this.navCtrl.navigateForward('/user-profile');
  }

  private async close() {
    await this.menuCtrl.close();
  }
}
