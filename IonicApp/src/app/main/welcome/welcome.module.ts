import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { GeolocationReaderModule } from 'src/app/shared/components/geolocation-reader/geolocation-reader.module';
import { WelcomeComponent } from './welcome.component';

const routes: Routes = [
    {
        path: '',
        component: WelcomeComponent
    }
];

@NgModule({
    declarations: [
        WelcomeComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),
        GeolocationReaderModule
    ]
})
export class WelcomeModule
{ }