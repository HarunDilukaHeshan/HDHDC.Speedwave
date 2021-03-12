import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { ConfigStateService } from '@abp/ng.core';
import { UserRolesConsts } from "../constants/user-roles-consts";
import { NavController } from "@ionic/angular";
import { Injectable } from "@angular/core";
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class LoginRouteGuard implements CanActivate {
    constructor(
        private oAuthService: OAuthService,
        private navCtrl: NavController,
        private configStateService: ConfigStateService){

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        var roles: Array<string> = this.configStateService.getDeep('currentUser.roles');
        var role = (roles)? roles[0] : '';

        if (!this.oAuthService.hasValidAccessToken()) {
            if (route.routeConfig.path != 'login')
                this.navCtrl.navigateForward('/login');
            return true;
        }

        console.log(roles);

        if (role == UserRolesConsts.admin) {
            this.navCtrl.navigateForward('/msys/aapp');
            console.log('Admin');
        }
        else if (role == UserRolesConsts.manager) {
            this.navCtrl.navigateForward('/msys/mapp');
            console.log('Manager');
        }
        else if (role == UserRolesConsts.rider) {
            this.navCtrl.navigateForward('/msys/rapp');
            console.log('Rider');
        }
        else if (role == UserRolesConsts.customer) {
            this.navCtrl.navigateForward('/msys/capp');
            console.log('Customer');
        }
        else 
            this.navCtrl.navigateForward('/exceptions/something-went-wrong');        

        console.log('Login Guard');         
        
        return false;
    }
}