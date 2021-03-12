import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainpageComponent } from './mainpage.component';
import { DetailComponent } from '../detail/detail.component';
import { CancelDialogComponent } from '../detail/cancel-dialog/cancel-dialog.component';
import { AvaiStoresComponent } from '../detail/avai-stores/avai-stores.component';

const routes: Routes = [
    {
        path: '',
        component: MainpageComponent              
    },
    {
        path: 'detail',
        component: DetailComponent
    }
];

@NgModule({
    declarations: [
        MainpageComponent,
        DetailComponent,
        CancelDialogComponent,
        AvaiStoresComponent
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),        
    ]
})
export class MainpageModule
{ }