import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UnitsComponent } from './units.component';
import { UnitsItemComponent } from './units-item/units-item.component';
import { FormComponent } from './form/form.component';

const routes: Routes = [
    {
        path: '',
        component: UnitsComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        UnitsComponent,
        UnitsItemComponent,
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
export class UnitsModule
{ }