import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ProvinceComponent } from './province.component';
import { FormComponent } from './form/form.component';

const routes: Routes = [
    {
        path: '',
        component: ProvinceComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        ProvinceComponent,
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
export class ProvinceModule
{ }