import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { IonicModule } from "@ionic/angular";
import { SomethingWentWrongComponent } from "./something-went-wrong/something-went-wrong.component";


const routes: Routes = [
    {
        path: 'something-went-wrong',
        component: SomethingWentWrongComponent
    }
];


@NgModule({
    declarations: [
        SomethingWentWrongComponent
    ],
    imports: [
        CommonModule,
        IonicModule,
        RouterModule.forChild(routes)
    ]
})
export class ExceptionPagesModule {

}