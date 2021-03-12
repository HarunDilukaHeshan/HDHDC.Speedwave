import { NgModule } from "@angular/core";
import { AdminRouteGuard } from "./admin-route-guard";
import { AuthorizedRouteGuard } from "./authorized-route-guard";
import { CustomerRouteGuard } from "./customer-route-guard";
import { LoginRouteGuard } from "./login-route-guard";
import { ManagerRouteGuard } from "./manager-route-guard";
import { RiderRouteGuard } from "./rider-route-guard";

@NgModule({
    providers: [
        AdminRouteGuard,
        ManagerRouteGuard,
        RiderRouteGuard,
        CustomerRouteGuard,
        LoginRouteGuard,
        AuthorizedRouteGuard
    ]
})
export class RouteGuardsModule {

}