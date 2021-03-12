import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { StoreChainComponent } from './store-chain.component';
import { IonicModule } from '@ionic/angular';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormComponent } from './form/form.component';
import { GalleryReaderModule } from 'src/app/shared/components/gallery-reader/gallery-reader.module';
import { ScItemComponent } from './sc-item/sc-item.component';

var routes: Routes = [
    {
        path: '',
        component: StoreChainComponent
    },
    {
        path: 'form',
        component: FormComponent
    },
    {
        path: 'branches',
        loadChildren: () => import('./store-branches/store-branches.module').then(m => m.StoreBranchesModule)
    }
];

@NgModule({
    declarations: [
        StoreChainComponent,
        FormComponent,
        ScItemComponent
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
export class StoreChainModule
{ }