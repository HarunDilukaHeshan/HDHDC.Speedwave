import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrderComponent } from './order.component';

const routes: Routes = [
    {
        path: '',
        component: OrderComponent,                
    }
];

@NgModule({
    declarations: [
        OrderComponent
    ],
    imports: [
        IonicModule,
        CommonModule,
        RouterModule.forChild(routes),        
    ]
})
export class OrderModule
{ }