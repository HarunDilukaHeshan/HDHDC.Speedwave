import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { IonicModule } from "@ionic/angular";
import { DetailsComponent } from "./details/details.component";
import { MyordersComponent } from "./myorders.component";

const routes: Routes = [
    {
        path: '',
        component: MyordersComponent
    },
    {
        path: 'details/:id',
        component: DetailsComponent
    }
];

@NgModule({
    declarations: [
        MyordersComponent,
        DetailsComponent
    ],
    imports: [
        CommonModule,
        IonicModule,
        RouterModule.forChild(routes)        
    ]
})
export class MyOrdersModule {

}