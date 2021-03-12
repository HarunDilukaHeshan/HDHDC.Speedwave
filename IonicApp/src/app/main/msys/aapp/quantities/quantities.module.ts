import { NgModule } from "@angular/core";
import { QuantitiesComponent } from './quantities.component';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { QuantityItemComponent } from './quantity-item/quantity-item.component';
import { FormComponent } from './form/form.component';

const routes: Routes = [
    {
        path: '',
        component: QuantitiesComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        QuantitiesComponent,
        QuantityItemComponent,
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
export class QuantitiesModule
{ }