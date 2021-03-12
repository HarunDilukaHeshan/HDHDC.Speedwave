import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';

const routes: Routes = [
    {
        path: '',
        component: HeaderComponent,
        children: [
            {
                path: '',
                loadChildren: () => import('./mainpage/mainpage.module').then(m => m.MainpageModule)
            },
            {
                path: 'categories',
                loadChildren: () => import('./categories/categories.module').then(m => m.CategoriesModule)
            },
            {
                path: 'items',
                loadChildren: () => import('./items/items.module').then(m => m.ItemsModule)
            },
            {
                path: 'quantities',
                loadChildren: () => import('./quantities/quantities.module').then(m => m.QuantitiesModule)
            },
            {
                path: 'units',
                loadChildren: () => import('./units/units.module').then(m => m.UnitsModule)
            }, 
            {
                path: 'mrt',
                loadChildren: () => import('./min-required-time/min-required-time.module').then(m => m.UnitsModule)
            },
            {
                path: 'storechain',
                loadChildren: () => import('./store-chain/store-chain.module').then(m => m.StoreChainModule)
            },
            {
                path: 'province',
                loadChildren: () => import('./province/province.module').then(m => m.ProvinceModule)
            },
            {
                path: 'district',
                loadChildren: () => import('./district/district.module').then(m => m.DistrictModule)
            },
            {
                path: 'city',
                loadChildren: () => import('./city/city.module').then(m => m.DistrictModule)
            },
            {
                path: 'promotions',
                loadChildren: () => import('./promotions/promotions.module').then(m => m.PromotionsModule)
            },
            {
                path: 'distance-charge',
                loadChildren: () => import('./distance-charge/distance-charge.module').then(m => m.DistanceChargeModule)
            },
            {
                path: 'delivery-schedule',
                loadChildren: () => import('./delivery-schedule/delivery-schedule.module').then(m => m.DeliveryScheduleModule)
            },
            {
                path: 'subtotal-percentage',
                loadChildren: () => import('./subtotal-percentage/subtotal-percentage.module').then(m => m.SubtotalPercentageModule)
            },
            {
                path: 'managers',
                loadChildren: () => import('./managers/managers.module').then(m => m.ManagersModule)
            },
            {
                path: 'slideshow',
                loadChildren: () => import('./slideshow/slideshow.module').then(m => m.SlideshowModule)
            }
        ]                
    }
];

@NgModule({
    declarations: [
        HeaderComponent
    ],
    imports: [
        RouterModule.forChild(routes),        
    ]
})
export class AappModule
{ }