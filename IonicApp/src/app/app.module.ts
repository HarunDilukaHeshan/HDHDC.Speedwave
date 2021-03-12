import { NgModule } from '@angular/core';
import { CoreModule } from '@abp/ng.core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
import { ThemeBasicModule } from '@abp/ng.theme.basic';
import { APP_ROUTE_PROVIDER } from './route.provider';

import { environment } from '../environments/environment';
import { NgxsModule } from '@ngxs/store';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { ThemeSharedModule } from '@abp/ng.theme.shared';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AccountConfigModule } from '@abp/ng.account/config';
import { CartState } from './state-management/state-containers/cart.state';
import { CartItem } from './shared/cart/cart.service';
import { PasswordResetPinRequestState } from './state-management/state-containers/password-reset-pin-request.state';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,      
    BrowserAnimationsModule,
    AppRoutingModule,     
    CoreModule.forRoot({
      environment,
    }),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    NgxsModule.forRoot([ CartState, PasswordResetPinRequestState ]),
    NgxsStoragePluginModule.forRoot({
      key: ['cartState', 'passwordResetPinRequest']
    }),
    ThemeBasicModule.forRoot(),
    IonicModule.forRoot(), 
  ],providers: [
    APP_ROUTE_PROVIDER,
    StatusBar,
    SplashScreen,
     { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
