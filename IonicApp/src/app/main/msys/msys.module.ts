import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
    {
        path: 'capp',
        loadChildren: () => import('./capp/capp.module').then(m => m.CappModule)
    },
    {
        path: 'aapp',
        loadChildren: () => import('./aapp/aapp.module').then(m => m.AappModule)
    },
    {
        path: 'mapp',
        loadChildren: () => import('./mapp/mapp.module').then(m => m.MappModule)
    },
    {
        path: 'rapp',
        loadChildren: () => import('./rapp/rapp.module').then(m => m.RappModule)
    }
];

@NgModule({
    declarations: [],
    imports: [
        RouterModule.forChild(routes),
        IonicModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
    ]
})
export class MsysModule
{ }