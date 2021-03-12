import { EventEmitter, Injectable } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Observable, Subject } from 'rxjs';
import { SpeedySelectComponent } from './speedy-select.component';

@Injectable({
    providedIn: 'root',
})
export class SpeedySelectController {

    constructor(
        private modalController: ModalController) 
        { }

    public async Create(options: SpeedySelectOptions) {
        var props = {
            options: options,
                    items: [],
                    events: {
                        onScrollReachBottom: new Subject(),
                        onSearchDidChange: new Subject()
                    }
        } as SpeedySelectProps;

        var modal = await this.modalController.create({
            component: SpeedySelectComponent,
            swipeToClose: true,
            showBackdrop: true,
            cssClass: (options.cssClass)? options.cssClass : '',
            componentProps: { 'props': props }                        
        });        

        return new SpeedySelectElement(modal, props);
    }    
}

export interface SpeedySelectOptions {
    title?: string;
    subTitle?: string;
    cssClass?: string;
    isMultiSelect?: boolean;    
}

export interface SpeedySelectItem {
    header: string;
    subHeader: string;
    selected?: boolean;
    disabled?: boolean;
    id?: number;
}

export interface SpeedySelectResults {
    addedItems: Array<SpeedySelectItem>;
    removedItems: Array<SpeedySelectItem>;
    items: Array<SpeedySelectItem>;
}

export interface SpeedySelectData {
    role: string;
    data: SpeedySelectResults;
}

export class SpeedySelectElement {

    constructor(
        private modal: HTMLIonModalElement,
        private props: SpeedySelectProps)
    { }

    public setItems(items: Array<SpeedySelectItem>) {
        if (!items || !Array.isArray(items)) throw Error('Argument exception');
        this.props.items = items;
    }

    public async present(): Promise<void> {        
        await this.modal.present();
    }

    public onDidDismiss(): Observable<SpeedySelectData> {
        return new Observable<SpeedySelectData>(ob => {
            this.modal.onDidDismiss().then((evd) => {
                if (evd.role == 'cancel' || evd.role == 'done')
                    ob.next({ role: evd.role, data: evd.data.data });
                ob.complete();
            }, 
            err => { ob.error(err); });
        });
    }

    public onSearchDidChange(): Observable<{ value: string }> {
        return new Observable<{ value: string }>(ob => {
            this.props.events.onSearchDidChange
                .subscribe(value => {
                    ob.next(value);
                }, err => {
                    ob.error(err);
                }, () => {
                    ob.complete();
                });
        });
    }

    public onScrollReachBottom(): Observable<void> {
        return new Observable<void>(ob => {
            this.props.events.onSearchDidChange.subscribe(() => {
                ob.next();
            }, err => {
                ob.error(err);
            }, () => {
                ob.complete();
            });
        });
    }
}

export interface SpeedySelectEvents {
    onSearchDidChange: Subject<{ value: string }>;
    onScrollReachBottom: Subject<{ value: string }>;
}

export interface SpeedySelectProps {
    options: SpeedySelectOptions;
    items: Array<SpeedySelectItem>;
    events: SpeedySelectEvents;
}