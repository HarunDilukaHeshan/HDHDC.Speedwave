import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { FormComponent } from './form/form.component';
import { MinRequiredTimeComponent } from './min-required-time.component';
import { MrtItemComponent } from './mrt-item/mrt-item.component';
import { DatetimeSpanPickerModule } from 'src/app/shared/components/datetime-span-picker/datetime-span-picker.module';

const routes: Routes = [
    {
        path: '',
        component: MinRequiredTimeComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        MinRequiredTimeComponent,        
        FormComponent,
        MrtItemComponent
    ],
    exports: [],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        IonicModule,
        DatetimeSpanPickerModule
    ]
})
export class UnitsModule
{ }