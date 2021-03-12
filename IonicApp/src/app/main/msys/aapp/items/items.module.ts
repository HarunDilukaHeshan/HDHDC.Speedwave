import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ItemsComponent } from './items.component';
import { ItemelComponent } from './itemel/itemel.component';
import { FormComponent } from './form/form.component';
import { GalleryReaderModule } from 'src/app/shared/components/gallery-reader/gallery-reader.module';
import { ProductPicsComponent } from './form/product-pics/product-pics.component';

const routes: Routes = [
    {
        path: '',
        component: ItemsComponent
    },
    {
        path: 'form',
        component: FormComponent
    }
];

@NgModule({
    declarations: [
        ItemsComponent,
        ItemelComponent,
        FormComponent,
        ProductPicsComponent
    ],
    exports: [],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        IonicModule,
        GalleryReaderModule
    ]
})
export class ItemsModule
{ }