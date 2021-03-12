import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DayOfTheWeek } from 'src/app/shared/DayOfTheWeek';
import { DayOfWeekHolder } from '../opening-days.component';

@Component({
  selector: 'app-dayoftheweek',
  templateUrl: './dayoftheweek.component.html',
  styleUrls: ['./dayoftheweek.component.scss'],
})
export class DayoftheweekComponent implements OnInit {

  @Input('label')
  private label: DayOfTheWeek;

  @Input('holder')
  private holder: DayOfWeekHolder;

  @Output('onChange')
  private onChangeEv: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  @Output('onDisabledChange')
  private onDisabledChangeEv: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor() 
  { }

  ngOnInit() 
  { }

  private onChange(ev: CustomEvent, isOpeningTime: boolean)
  {
    if (isOpeningTime)
      this.holder.openingTime = ev.detail.value;
    else
      this.holder.closingTime = ev.detail.value;

    this.onChangeEv.emit(new CustomEvent('onChange', { detail: { label: this.label } }));
  }

  private onDisabledChange(ev: CustomEvent)
  {
    this.holder.disabled = !this.holder.disabled;
    this.onDisabledChangeEv.emit(new CustomEvent('onChange', { 
      detail: { 
        label: this.label,
        disabled: this.holder.disabled 
      } 
    }));
  }

  private strToDay(day: DayOfTheWeek) { return DayOfTheWeek[day]; }
}
