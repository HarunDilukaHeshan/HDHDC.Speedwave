import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';

const routes: Routes = [
    {
        path: '',
        component: HeaderComponent,
        children: [
            {
                path: '',
                loadChildren: () => import('./mainpage/mainpage.module').then(m => m.MainpageModule)           
            }
        ]        
    }
];

@NgModule({
    declarations: [
        HeaderComponent
    ],
    imports: [
        RouterModule.forChild(routes),        
    ]
})
export class RappModule
{ }