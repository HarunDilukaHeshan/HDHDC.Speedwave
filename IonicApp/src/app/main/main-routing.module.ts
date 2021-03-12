import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginRouteGuard } from '../route-guards/login-route-guard';
import { RouteGuardsModule } from '../route-guards/route-guards.module';

const routes: Routes = [
    {
        path: '',
        canActivate: [LoginRouteGuard]
    },
    {
        path: 'msys',
        loadChildren: () => import('./msys/msys.module').then(m => m.MsysModule)
    },
    {
        path: 'login',
        loadChildren: () => import('./login/login.module').then(m => m.LoginModule),
        canActivate: [LoginRouteGuard]
    },
    {
        path: 'register',
        loadChildren: () => import('./register/register.module').then(m => m.RegisterModule)
    },
    {
        path: 'welcome',
        loadChildren: () => import('./welcome/welcome.module').then(m => m.WelcomeModule)
    },
    {
        path: 'user-profile',
        loadChildren: () => import('./user-profile/user-profile.module').then(m => m.UserProfileModule)
    },
    {
        path: 'forgot-password',
        loadChildren: () => import('./forgot-password/forgot-password.module').then(m => m.ForgotPasswordModule)
    }
];

@NgModule({
    declarations: [],
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule,
        RouteGuardsModule
    ]
})
export class MainRoutingModule
{ }