import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryReader02Module } from 'src/app/shared/components/gallery-reader02/gallery-reader02.module';
import { CartComponent } from './cart.component';

const routes: Routes = [
    {
        path: '',
        component: CartComponent
    }
];

@NgModule({
    declarations: [
        CartComponent
    ],
    imports: [
        CommonModule,
        GalleryReader02Module,
        RouterModule.forChild(routes)
    ]
})
export class CartModule
{ }