import { Injectable } from "@angular/core";
import { MenuController } from '@ionic/angular';

@Injectable({ providedIn: 'root' })
export class SideMenuController {

    private menuId: string = 'side-menu';

    constructor(
        private menuCtrl: MenuController) 
    { }

    public async open() {
        await this.menuCtrl.enable(true, this.menuId);
        await this.menuCtrl.open(this.menuId);
    }

    public async close() {
        await this.menuCtrl.close(this.menuId);
        await this.menuCtrl.enable(false, this.menuId);
    }
}