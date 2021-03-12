import { Directive, DoCheck, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { ConfigStateService } from '@abp/ng.core';
import { UserRolesConsts } from 'src/app/constants/user-roles-consts';

@Directive({
  selector: '[adminGuard]'
})
export class AdminGuardDirective implements DoCheck {

  private hasView = false;

  constructor(
    private oAuthService: OAuthService,
    private configStateService: ConfigStateService,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef) {             
  }

  ngDoCheck() {
    if (!this.oAuthService.hasValidAccessToken) return;
    var roles: Array<string> = this.configStateService.getDeep('currentUser.roles');
    var hasRole = roles.find(v => v == UserRolesConsts.admin) != undefined;

    if (hasRole && !this.hasView) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    }
    else if (!hasRole && this.hasView) {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }
}
