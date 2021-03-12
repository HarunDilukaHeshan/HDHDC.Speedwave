import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { SideMenuModule } from '../../side-menu/side-menu.module';

const routes: Routes = [
    {
        path: '',
        component: HeaderComponent,
        children: [
            {
                path: '',
                loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
            },
            {
                path: 'item',
                loadChildren: () => import('./item/item.module').then(m => m.ItemModule)
            },
            {
                path: 'search',
                loadChildren: () => import('./search/search.module').then(m => m.SearchModule)
            },
            {
                path: 'cart',
                loadChildren: () => import('./cart/cart.module').then(m => m.CartModule)
            },
            {
                path: 'checkout',
                loadChildren: () => import('./checkout/checkout.module').then(m => m.CheckoutModule)
            },
            {
                path: 'my-orders',
                loadChildren: () => import('./myorders/myorders.module').then(m => m.MyOrdersModule)
            }
        ]                
    }
];

@NgModule({
    declarations: [
        HeaderComponent
    ],
    exports: [
        RouterModule
    ],
    imports: [
        CommonModule,
        SideMenuModule,
        RouterModule.forChild(routes),        
    ]
})
export class CappModule
{ }