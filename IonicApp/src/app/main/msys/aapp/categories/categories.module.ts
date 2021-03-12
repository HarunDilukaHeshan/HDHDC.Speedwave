import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoriesComponent } from './categories.component';
import { CatItemComponent } from './cat-item/cat-item.component';
import { CatFormComponent } from './cat-form/cat-form.component';
import { GalleryReaderModule } from 'src/app/shared/components/gallery-reader/gallery-reader.module';

const routes: Routes = [
    {
        path: '',
        component: CategoriesComponent
    },
    {
        path: 'form',
        component: CatFormComponent
    }
];

@NgModule({
    declarations: [
        CategoriesComponent,
        CatItemComponent,
        CatFormComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        IonicModule,
        GalleryReaderModule       
    ]
})
export class CategoriesModule
{ }