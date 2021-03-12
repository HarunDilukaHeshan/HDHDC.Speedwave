import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { FormComponent } from './form/form.component';
import { DeliveryScheduleComponent } from './delivery-schedule.component';
import { DatetimeSpanPickerModule } from 'src/app/shared/components/datetime-span-picker/datetime-span-picker.module';

const routes: Routes = [
    {
        path: '',
        component: DeliveryScheduleComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        FormComponent,
        DeliveryScheduleComponent
    ],
    exports: [],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        IonicModule,
        DatetimeSpanPickerModule
    ]
})
export class DeliveryScheduleModule
{ }