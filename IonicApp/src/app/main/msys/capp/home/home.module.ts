import { NgModule } from "@angular/core";
import { HomeComponent } from './home.component';
import { Routes, RouterModule } from '@angular/router';
import { SlideshowComponent } from './sub-components/slideshow/slideshow.component';
import { CategoriesComponent } from './sub-components/categories/categories.component';
import { ItemsComponent } from './sub-components/items/items.component';
import { CommonModule } from '@angular/common';
import { AppImgModule } from 'src/app/shared/components/app-img/app-img.module';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    }    
];

@NgModule({
    declarations: [
        HomeComponent,
        SlideshowComponent,
        CategoriesComponent,
        ItemsComponent
    ],
    imports: [
        CommonModule,
        AppImgModule,
        RouterModule.forChild(routes)
    ]
})
export class HomeModule
{ }