import { Directive, ElementRef, HostListener, Output, EventEmitter } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Directive({
    selector: '[trigger]'
})
export class TriggerDirective {    
    public clickEventHandlers: Subject<Event> = new Subject<Event>();

    @HostListener('click')
    public onClick(ev: Event)
    {
        this.clickEventHandlers.next(ev);
    }

    constructor(public triggerElement: ElementRef)
    { }            

    onDestroy()
    {
        this.clickEventHandlers.complete();
    }
}