import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { StoreOpenDayDto } from 'src/app/proxy/dtos';
import { DayOfTheWeek } from 'src/app/shared/DayOfTheWeek';

@Component({
  selector: 'app-opening-days',
  templateUrl: './opening-days.component.html',
  styleUrls: ['./opening-days.component.scss'],
})
export class OpeningDaysComponent implements OnInit, OnChanges {  

  private dowArr: Array<DayOfWeekHolder> = [];  

  @Input('openDayDtoArr')
  private openDayDtoArr: Array<StoreOpenDayDto> = [];

  @Output('onChange')
  private changeEv: EventEmitter<CustomEvent<DayOfWeekHolder[]>> = new EventEmitter<CustomEvent<DayOfWeekHolder[]>>();

  @Output('onReady')
  private readyEv: EventEmitter<CustomEvent<DayOfWeekHolder[]>> = new EventEmitter<CustomEvent<DayOfWeekHolder[]>>();

  constructor() 
  { }

  ngOnInit() {
    this.setupHolders();
  }

  ngOnChanges() {
    this.synchronizeDtos();
  }

  private synchronizeDtos() {
    if (this.openDayDtoArr && this.openDayDtoArr.length > 0) {

      this.dowArr.forEach(val => { val.disabled = true; });

      this.openDayDtoArr.forEach(dto => {
        var holder = this.dowArr.find(val => val.dayOfWeek == dto.dayOfWeek);
        if (holder) {          
          holder.openingTime = dto.openingTime;
          holder.closingTime = dto.closingTime;
          holder.disabled = false;
        }
        else
          throw Error('Invalid open day dtos');
      });

      var arr = this.getNewDowArr();
      this.readyEv.emit(new CustomEvent<DayOfWeekHolder[]>('onReady', { detail: arr }));
    }
  }

  private setupHolders() {
    for (var i = 0; i < 7; i++)
      this.dowArr.push(new DayOfWeekHolder(i));

    var arr = this.getNewDowArr();
    this.readyEv.emit(new CustomEvent<DayOfWeekHolder[]>('onReady', { detail: arr }));
  }

  private onValueChange(ev: CustomEvent) {
    var arr = this.getNewDowArr();
    this.changeEv.emit(new CustomEvent<DayOfWeekHolder[]>('onChange', { detail: arr }));
  }

  private onDisabledChange(ev: CustomEvent)
  {
    this.onValueChange(ev);
  }

  private getNewDowArr()
  {
    var arr = new Array<DayOfWeekHolder>();
    this.dowArr.forEach(val => {
      arr.push(new DayOfWeekHolder(val.dayOfWeek, val.openingTime, val.closingTime, val.disabled));
    });
    return arr;
  }  
}

export class DayOfWeekHolder
{
  constructor(
    public dayOfWeek: DayOfTheWeek,
    public openingTime: string = '06:30',
    public closingTime: string = '22:30',
    public disabled: boolean = false)
  { }
}