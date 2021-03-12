import { AfterViewInit, ApplicationRef, Component, ComponentFactoryResolver, Injector, OnDestroy, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { NavController } from '@ionic/angular';
import { SideMenuController } from 'src/app/main/side-menu/side-menu-controller.service';
import { CartService } from 'src/app/shared/cart/cart.service';
import { DomPortalHost, DomPortalOutlet, PortalHost, TemplatePortal } from '@angular/cdk/portal';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']  
})
export class HeaderComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('forSideMenu')
  private templatePC: TemplateRef<unknown>; 

  private tPortal: TemplatePortal<unknown>;
  private domPortalOutlet: DomPortalOutlet;

  constructor(
    private _componentFactoryResolver: ComponentFactoryResolver,
    private _injector: Injector,
    private _appRef: ApplicationRef,
    private _viewContainerRef: ViewContainerRef,
    private navController: NavController,
    private sideMenuCtrl: SideMenuController,
    private cartService: CartService) 
  { }

  ngOnInit() 
  { }

  ngAfterViewInit() {
    this.tPortal = new TemplatePortal(this.templatePC, this._viewContainerRef);
    
    this.domPortalOutlet = new DomPortalOutlet(
      document.querySelector('#side-menu-custom-content-holder'),
      this._componentFactoryResolver,
      this._appRef,
      this._injector);

    this.domPortalOutlet.attach(this.tPortal);
  }

  ngOnDestroy() {
    this.domPortalOutlet.detach();
  }

  private moveToCart() {
    this.navController.navigateForward(['msys/capp/cart']);
  }

  private async open() {
    await this.sideMenuCtrl.open();
  }

  private async moveToMyOrders() {
    await this.sideMenuCtrl.close();
    await this.navController.navigateForward('msys/capp/my-orders');
  }

  private async moveToHome() {
    await this.sideMenuCtrl.close();
    await this.navController.navigateForward('msys/capp');
  }
}
