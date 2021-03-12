import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { GalleryReader02Component } from './gallery-reader02.component';

@NgModule({
    declarations: [
        GalleryReader02Component
    ],
    exports: [
        GalleryReader02Component
    ],
    imports: [
        CommonModule
    ]
})
export class GalleryReader02Module 
{ }