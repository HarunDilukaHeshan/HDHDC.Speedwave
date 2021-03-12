import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { GeolocationReaderModule } from 'src/app/shared/components/geolocation-reader/geolocation-reader.module';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { CustomerAddressComponent } from './customer-address/customer-address.component';
import { FormComponent } from './customer-address/form/form.component';
import { RiderInfoComponent } from './rider-info/rider-info.component';
import { UserProfileComponent } from './user-profile.component';

const routes: Routes = [
    {
        path: '',
        component: UserProfileComponent
    },
    {
        path: 'change-password',
        component: ChangePasswordComponent
    },
    {
        path: 'customer-address',
        children: [
            {
                path: '',
                component: CustomerAddressComponent
            },
            {
                path: 'form',
                component: FormComponent
            }
        ]        
    },
    {
        path: 'rider-info',
        component: RiderInfoComponent
    }
];

@NgModule({
    declarations: [
        UserProfileComponent,
        ChangePasswordComponent,
        CustomerAddressComponent,
        RiderInfoComponent,
        FormComponent
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
export class UserProfileModule
{ }