import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AdminGuardDirective } from "./admin-guard-directive.directive";
import { CustomerGuardDirective } from "./customer-guard-directive.directive";
import { ManagerGuardDirective } from "./manager-guard-directive.directive";
import { RiderGuardDirective } from "./rider-guard-directive.directive";

@NgModule({
    declarations: [
        AdminGuardDirective,
        ManagerGuardDirective,
        RiderGuardDirective,
        CustomerGuardDirective
    ],
    exports: [
        AdminGuardDirective,
        ManagerGuardDirective,
        RiderGuardDirective,
        CustomerGuardDirective
    ],
    imports: [
        CommonModule
    ]
})
export class GuardDirectivesModule {

}