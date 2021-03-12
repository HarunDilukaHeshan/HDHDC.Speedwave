import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { AppImgComponent } from './app-img.component';

@NgModule({
    declarations: [
        AppImgComponent
    ],
    exports: [
        AppImgComponent
    ],
    imports: [
        CommonModule
    ]
})
export class AppImgModule { }