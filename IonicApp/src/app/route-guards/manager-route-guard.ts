import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { ConfigStateService } from '@abp/ng.core';
import { UserRolesConsts } from "../constants/user-roles-consts";
import { NavController } from "@ionic/angular";
import { Injectable } from "@angular/core";

@Injectable()
export class ManagerRouteGuard implements CanActivate {
    constructor(
        private navCtrl: NavController,
        private configStateService: ConfigStateService){

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        var roles: Array<string> = this.configStateService.getOne('currentUser.roles');
        if (roles.find(v => v == UserRolesConsts.manager)) return true;        
        this.navCtrl.back();
    }    
}