import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RiderComponent } from './rider.component';
import { FormComponent } from './form/form.component';
import { GeolocationReaderModule } from 'src/app/shared/components/geolocation-reader/geolocation-reader.module';

const routes: Routes = [
    {
        path: '',
        component: RiderComponent               
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        RiderComponent,
        FormComponent
    ],
    imports: [
        IonicModule,
        CommonModule,
        ReactiveFormsModule,
        GeolocationReaderModule,
        RouterModule.forChild(routes),        
    ]
})
export class RiderModule
{ }