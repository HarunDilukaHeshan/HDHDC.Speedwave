import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppImgModule } from 'src/app/shared/components/app-img/app-img.module';
import { ProceedComponent } from './proceed.component';

const routes: Routes = [
    {
        path: '',
        component: ProceedComponent
    }    
];

@NgModule({
    declarations: [
        ProceedComponent
    ],
    imports: [
        CommonModule,
        AppImgModule,
        RouterModule.forChild(routes)
    ]
})
export class ProceedModule
{ }