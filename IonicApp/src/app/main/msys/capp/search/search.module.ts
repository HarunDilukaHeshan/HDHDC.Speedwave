import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SearchComponent } from './search.component';
import { CommonModule } from '@angular/common';
import { AppImgModule } from 'src/app/shared/components/app-img/app-img.module';

const routes: Routes = [
    {
        path: '',
        component: SearchComponent
    }    
];

@NgModule({
    declarations: [
        SearchComponent
    ],
    imports: [
        CommonModule,
        AppImgModule,
        RouterModule.forChild(routes)
    ]
})
export class SearchModule
{ }