import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { ConfigStateService } from '@abp/ng.core';
import { UserRolesConsts } from "../constants/user-roles-consts";
import { NavController } from "@ionic/angular";
import { Injectable } from "@angular/core";
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class AuthorizedRouteGuard implements CanActivate {
    constructor(
        private oAuthService: OAuthService,
        private navCtrl: NavController,
        private configStateService: ConfigStateService) {

    }
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if(!this.oAuthService.hasValidAccessToken) { 
            this.navCtrl.navigateForward('login');
            return false;
        }

        return true;
    }
}