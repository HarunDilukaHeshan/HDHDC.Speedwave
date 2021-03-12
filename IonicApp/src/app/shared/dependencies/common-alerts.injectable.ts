import { Injectable } from "@angular/core";
import { AlertController } from '@ionic/angular';

@Injectable({providedIn: 'root'})
export class CommonAlerts
{
    constructor(private alertsController: AlertController)
    { }

    public async warningAlert(
        header: string,
        subHeader: string,
        message: string, 
        buttonsGroup: CommonAlertsButtonGroups)
        : Promise<CommonAlertsButtons>
    {
        var response: CommonAlertsButtons;
        
        switch(buttonsGroup)
        {
            case CommonAlertsButtonGroups.YesNo:
                response = await this.warningYesNo(header, subHeader, message);
            break;
        }
        
        return response;
    }

    public async errorAlert(
        header: string,
        subHeader: string,
        message: string, 
        buttonsGroup: CommonAlertsButtonGroups)
        : Promise<CommonAlertsButtons>
    {
        var response: CommonAlertsButtons;
        
        switch(buttonsGroup)
        {
            case CommonAlertsButtonGroups.Ok:
                response = await this.errorOk(header, subHeader, message);
            break;
        }
        
        return response;
    }

    private async errorOk(header: string, subHeader: string, message: string)
        : Promise<CommonAlertsButtons>
    {
        var alert = await this.alertsController.create({
            header: header,
            subHeader: subHeader,
            message: message,
            backdropDismiss: false,
            buttons: [
                {
                    text: 'Ok',
                    role: 'ok',
                    handler: () => {}
                }
            ]
        });
        
        await alert.present();
        await alert.onDidDismiss();

        return CommonAlertsButtons.Ok;
    }
    

    private async warningYesNo(header: string, subHeader: string, message: string)
        : Promise<CommonAlertsButtons>
    {
        var alert = await this.alertsController.create({
            header: header,
            subHeader: subHeader,
            message: message,
            backdropDismiss: false,
            buttons: [
                {
                    text: 'Yes',
                    role: 'yes',
                    handler: () => {}
                },
                {
                    text: 'No',
                    role: 'no',
                    handler: () => {}                
                }
            ]
        });
        
        await alert.present();
        var evData = await alert.onDidDismiss();
        if (evData.role == 'yes')
            return CommonAlertsButtons.Yes;
        else
            return CommonAlertsButtons.No;
    }
}

export enum CommonAlertsButtonGroups
{
    YesNo, OkCancel, Ok, Dismiss
}

export enum CommonAlertsButtons
{
    Yes, No, Ok, Cancel, Dismiss
}