import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { IonicModule } from "@ionic/angular";
import { GalleryReader02Module } from "src/app/shared/components/gallery-reader02/gallery-reader02.module";
import { SlideComponent } from "./slide/slide.component";
import { SlideshowComponent } from "./slideshow.component";

const routes: Routes = [
    {
        path: "",
        component: SlideshowComponent
    },
    {
        path: 'form',
        component: SlideComponent
    }
];

@NgModule({
    declarations: [
        SlideshowComponent,
        SlideComponent        
    ],
    imports: [
        CommonModule,
        IonicModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes)
    ]
})
export class SlideshowModule {    
}