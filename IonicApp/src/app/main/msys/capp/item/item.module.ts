import { ItemComponent } from './item.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryReader02Module } from 'src/app/shared/components/gallery-reader02/gallery-reader02.module';
import { ThumbComponent } from './thumb/thumb.component';

const routes: Routes = [
    {
        path: '',
        component: ItemComponent
    }    
];

@NgModule({
    declarations: [
        ItemComponent,
        ThumbComponent
    ],
    imports: [
        CommonModule,
        GalleryReader02Module,
        RouterModule.forChild(routes)
    ]
})
export class ItemModule
{ }