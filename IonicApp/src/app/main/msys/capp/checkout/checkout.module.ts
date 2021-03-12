import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppImgModule } from 'src/app/shared/components/app-img/app-img.module';
import { CheckoutComponent } from './checkout.component';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
    {
        path: '',
        component: CheckoutComponent
    }    
];

@NgModule({
    declarations: [
        CheckoutComponent
    ],
    imports: [
        CommonModule,
        AppImgModule,
        FormsModule,
        RouterModule.forChild(routes)
    ]
})
export class CheckoutModule
{ }