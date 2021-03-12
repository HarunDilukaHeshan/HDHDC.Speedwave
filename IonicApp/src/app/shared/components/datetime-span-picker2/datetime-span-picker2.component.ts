import { Component, EventEmitter, forwardRef, Input, OnChanges, OnInit, Output } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { PickerController } from '@ionic/angular';
import { PickerOptions, PickerColumn, PickerButton, PickerColumnOption } from '@ionic/core';

@Component({
  selector: 'app-datetime-span-picker2',
  templateUrl: './datetime-span-picker2.component.html',
  styleUrls: ['./datetime-span-picker2.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatetimeSpanPicker2Component),
      multi: true
    }
  ]
})
export class DatetimeSpanPicker2Component implements ControlValueAccessor, OnChanges, OnInit {

  private displayStr: string;

  private currentSelection: PickerSelection;

  private disabled: boolean = false;

  @Input('label')
  private label: string;

  @Input('value')
  private value: string = '';

  @Input('maxDays')
  private maxDays: number = 7;

  @Output("onChange")
  private changeEv: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  @Output("ionClick")
  private clickEv: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor(
    private pickerController: PickerController) 
  { }  

  ngOnChanges() {
    if (this.value != '') {
      this.setValue(this.value);
      this.displayResult();
      this.changeEv.emit(new CustomEvent('onChange', { detail: this.currentSelection }));
    }
  }

  ngOnInit() {}  

  public async open()
  {
    this.onTouched();
    var options = this.getOptions();
    var picker = await this.pickerController.create(options);      
    this.clickEv.emit(new CustomEvent('onClick'));        

    await picker.present();

    var detail = await picker.onDidDismiss();

    if (detail.role == 'done') {
      this.SetResult(detail.data);
      this.displayResult();
      this.changeEv.emit(new CustomEvent('onChange', { detail: this.currentSelection }));
      this.onValueChange(this.currentSelection);      
    }     
  }

  private SetResult(data: any) {
    var days = data['Days'];
    var hours = data['Hours'];
    var minutes = data['Minutes'];

    try {
      var daysCD = new ColumnDetail(days.text, days.value, days.columnIndex);
      var hoursCD = new ColumnDetail(hours.text, hours.value, hours.columnIndex);
      var minutesCD = new ColumnDetail(minutes.text, minutes.value, minutes.columnIndex);
      this.currentSelection = new PickerSelection(daysCD, hoursCD, minutesCD);      
    }
    catch { console.error('Internal error occured in the date time span picker'); }
  }

  private displayResult() {
    var cs = this.currentSelection;
    var str = cs.days.text + ' Days, '  +  cs.hours.text + ' Hours, ' +  cs.minutes.text + ' Minutes';
    this.displayStr = str;
  }

  private setValue(value: string) {    
    try {
    var val = (value.match(/:/g).length == 2)? value.substring(0, value.lastIndexOf(':')) : value;

    var days = (val.indexOf('.') == -1)? '0' : val.substr(0, val.indexOf('.'));
    var hours = val.substring(val.indexOf('.') + 1, val.indexOf(':'));
    var minutes = val.substr(val.indexOf(':') + 1);
    var daysCD = new ColumnDetail(days, parseInt(days), 0);
    var hoursCD = new ColumnDetail(hours, parseInt(hours), 1);
    var minutesCD = new ColumnDetail(minutes, parseInt(minutes), 2);
    this.currentSelection = new PickerSelection(daysCD, hoursCD, minutesCD);
    } 
    catch { throw Error('Invalid value'); }
  }

  private getOptions()
  {
    var columns: Array<PickerColumn> = new Array<PickerColumn>(3);
    var buttons: Array<PickerButton> = new Array<PickerButton>(2);
    
    var sDay = (this.currentSelection)? this.currentSelection.days.value : 0;
    var sHour = (this.currentSelection)? this.currentSelection.hours.value : 0;    
    var sMinute = (this.currentSelection)? this.currentSelection.minutes.value : 0;    

    columns[0] = { name: 'Days', prefix: 'Days', options: [], selectedIndex: sDay } as PickerColumn;
    columns[1] = { name: 'Hours', prefix: 'HH', options: [], selectedIndex: sHour } as PickerColumn;
    columns[2] = { name: 'Minutes', prefix: 'mm', options: [], selectedIndex: sMinute } as PickerColumn;

    buttons[0] = { text: 'Cancel', role: 'cancel'} as PickerButton;
    buttons[1] = { text: 'Done', role: 'done'} as PickerButton;

    for (var i = 0; i < this.maxDays; i++) {
      var columnOption = { text: i.toString(), value: i } as PickerColumnOption;
      columns[0].options.push(columnOption); 
    }
    
    for (var i = 0; i < 24; i++) {
      var columnOption = { text: i.toString(), value: i } as PickerColumnOption;
      columns[1].options.push(columnOption); 
    }

    for (var i = 0; i < 60; i++) {
      var columnOption = { text: i.toString(), value: i } as PickerColumnOption;
      columns[2].options.push(columnOption); 
    }

    return  { columns: columns, buttons: buttons } as PickerOptions;
  }

  private onValueChange = (pickerSelection: PickerSelection) => {};

  private onTouched = () => {};

  writeValue(obj: any): void {

    if (obj == '') return;

    this.value = obj as string;        
    this.setValue(this.value);
    this.displayResult();
    this.changeEv.emit(new CustomEvent('onChange', { detail: this.currentSelection }));
    this.onValueChange(this.currentSelection);
  }

  registerOnChange(fn: any): void {
    this.onValueChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }  
}

export class PickerSelection
{
  constructor(
    private readonly _days: ColumnDetail,
    private readonly _hours: ColumnDetail,
    private readonly _minutes: ColumnDetail)
  { }

  public get days() { return this._days; }
  public get hours() { return this._hours; }
  public get minutes() { return this._minutes; }
}

export class ColumnDetail
{  
  constructor(
    private readonly _text: string, 
    private readonly _value: number, 
    private readonly _columnIndex: number)
  { }

  public get text() { return this._text; }
  public get value() { return this._value; }
  public get columnIndex() { return this._columnIndex; }
}