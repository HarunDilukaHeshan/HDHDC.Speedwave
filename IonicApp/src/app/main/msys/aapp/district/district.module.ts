import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { FormComponent } from './form/form.component';
import { DistrictComponent } from './district.component';

const routes: Routes = [
    {
        path: '',
        component: DistrictComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        DistrictComponent,
        FormComponent
    ],
    exports: [],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        IonicModule
    ]
})
export class DistrictModule
{ }