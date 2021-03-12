import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormComponent } from './form/form.component';
import { FormComponent as ClosingDateFormComponent } from './closing-dates/form/form.component';
import { StoreBranchesComponent } from './store-branches.component';
import { BasicInfoComponent } from './form/basic-info/basic-info.component';
import { OpeningDaysComponent } from './form/opening-days/opening-days.component';
import { DayoftheweekComponent } from './form/opening-days/dayoftheweek/dayoftheweek.component';
import { ClosingDatesComponent } from './closing-dates/closing-dates.component';
import { GeolocationReaderModule } from 'src/app/shared/components/geolocation-reader/geolocation-reader.module';
import { ItemBranchComponent } from './item-branch/item-branch.component';
import { SpeedySelectModule } from 'src/app/shared/components/speedy-select/speedy-select.module';
import { AddBranchItemsComponent } from './item-branch/add-branch-items/add-branch-items.component';



var routes: Routes = [
    {
        path: '',
        component: StoreBranchesComponent
    },
    {
        path: 'form',
        component: FormComponent
    },
    {
        path: 'closing-dates',
        children: [
            {
                path: '',
                component: ClosingDatesComponent
            },
            {
                path: 'form',
                component: ClosingDateFormComponent
            }
        ]
    },
    {
        path: 'item-branch',
        component: ItemBranchComponent
    }
];

@NgModule({
    declarations: [
        StoreBranchesComponent,
        FormComponent,
        BasicInfoComponent,
        OpeningDaysComponent,
        DayoftheweekComponent,
        ClosingDatesComponent,
        ClosingDateFormComponent,
        ItemBranchComponent,
        AddBranchItemsComponent        
    ],
    exports: [],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        ReactiveFormsModule,
        IonicModule,
        GeolocationReaderModule,
        SpeedySelectModule
    ]
})
export class StoreBranchesModule
{ }