import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { SideMenuComponent } from './side-menu.component';

@NgModule({
    declarations: [
        SideMenuComponent
    ],
    exports: [
        SideMenuComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        ReactiveFormsModule,
        RouterModule
    ]
})
export class SideMenuModule
{ }