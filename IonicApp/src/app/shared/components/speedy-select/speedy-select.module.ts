import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { SpeedySelectComponent } from './speedy-select.component';

@NgModule({
    declarations: [
        SpeedySelectComponent
    ],
    exports: [
        SpeedySelectComponent
    ],
    imports: [
        CommonModule
    ]
})
export class SpeedySelectModule
{ }