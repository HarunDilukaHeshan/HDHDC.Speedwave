import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainpageComponent } from './mainpage.component';

const routes: Routes = [
    {
        path: '',
        component: MainpageComponent              
    }
];

@NgModule({
    declarations: [
        MainpageComponent
    ],
    imports: [
        RouterModule.forChild(routes),        
    ]
})
export class MainpageModule
{ }