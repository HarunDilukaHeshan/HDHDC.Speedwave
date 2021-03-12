import { Reference } from '@angular/compiler/src/render3/r3_ast';
import { AfterContentInit, AfterViewInit, ChangeDetectorRef, Component, DoCheck, Input, OnChanges, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { SpeedySelectData, SpeedySelectEvents, SpeedySelectItem, SpeedySelectOptions, SpeedySelectProps } from './speedy-select.controller.service';

@Component({
  selector: 'app-speedy-select',
  templateUrl: './speedy-select.component.html',
  styleUrls: ['./speedy-select.component.scss'],
})
export class SpeedySelectComponent implements DoCheck, OnInit {

  @Input() private props: SpeedySelectProps;

  private isMultiSelect: boolean = false;  

  constructor(private modalCtrl: ModalController) 
  { }

  ngDoCheck() 
  { }

  ngOnInit() {         
    if (!this.hasValidProps) throw Error('Invalid props');
    this.isMultiSelect = (this.props.options.isMultiSelect)? this.props.options.isMultiSelect : false;
  }

  private onSearchbarChange(ev: CustomEvent<{ value: string }>) {    
    if (!this.props.events || !this.props.events.onSearchDidChange) throw Error('Invalid speedyEvents');
    this.props.events.onSearchDidChange.next({ value: ev.detail.value });    
  }

  private onScrollReachEnd() {
    if (!this.props.events || !this.props.events.onScrollReachBottom) throw Error('Invalid speedyEvents');
    this.props.events.onScrollReachBottom.next();
  }

  private hasValidProps () {
    var invalid = false;
    invalid = !(this.props && this.props.options && this.props.items && this.props.events);
    invalid = (invalid || !Array.isArray(this.props.items));    
    return !(invalid || !(this.props.events.onScrollReachBottom && this.props.events.onSearchDidChange));
  }

  private select(item: SpeedySelectItem) {
    item.selected = !(item.selected && item.selected == true);
  }

  private cancel() {
    var r =  { role: 'cancel', data: { } } as SpeedySelectData;
    this.modalCtrl.dismiss(r, 'cancel');
  }

  private done() {
    var arr = this.getItemArr();
    var r =  { role: 'done', data: { addedItems: arr } } as SpeedySelectData;
    this.modalCtrl.dismiss(r, 'done');
  }

  private getItemArr() {
    var arr: Array<SpeedySelectItem> = [];
    this.props.items.forEach(val => {
      if (val.selected && val.selected == true && val.disabled == false) arr.push(val);
    });

    return arr;
  }
}