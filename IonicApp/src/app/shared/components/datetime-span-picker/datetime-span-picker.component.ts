import { Component, OnInit, ViewChild, Output, EventEmitter, Input, OnChanges } from '@angular/core';
import { PickerOptions, PickerColumn, PickerButton, PickerColumnOption } from '@ionic/core';
import { PickerController, IonInput } from '@ionic/angular';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-datetime-span-picker',
  templateUrl: './datetime-span-picker.component.html',
  styleUrls: ['./datetime-span-picker.component.scss'],
})
export class DatetimeSpanPickerComponent implements OnInit, OnChanges {
  
  private strDisplayLabel: string = "";  

  private currentSelection: PickerSelection = null;

  private minPickerValue: PickerSelection = null;

  private curPickerValue: PickerSelection = null;

  @Input('label')
  private label: string = '';

  @Input('formControl')
  private formControl: FormControl;

  @Input('minValue')
  private minValue: string = '00.00:00';

  @Input('value')
  private value: string = '00.00:00';

  @Output('onChange')
  private onChangeEvent: EventEmitter<CustomEvent<PickerSelection>> = new EventEmitter<CustomEvent<PickerSelection>>()  

  constructor(
    private pickerController: PickerController) 
  { }

  ngOnChanges() {
    this.setupMinValue(this.minValue);
    this.setupValue(this.value);
  }

  ngOnInit() { 

  }  

  private setupMinValue(value: string)
  {
    var parsedVal = this.parseValue(value);

    if (parsedVal == null) throw Error('Invalid min value');

    var daysCD = new ColumnDetail(parsedVal.days, parseInt(parsedVal.days), parseInt(parsedVal.days));
    var hoursCD = new ColumnDetail(parsedVal.hours, parseInt(parsedVal.hours), parseInt(parsedVal.hours));
    var minutesCD = new ColumnDetail(parsedVal.minutes, parseInt(parsedVal.minutes), parseInt(parsedVal.minutes));

    this.minPickerValue = new PickerSelection(daysCD, hoursCD, minutesCD);
  }

  private setupValue(value: string)
  {
    var parsedVal = this.parseValue(value);

    if (parsedVal == null) throw Error('Invalid value');

    var daysCD = new ColumnDetail(parsedVal.days, parseInt(parsedVal.days), parseInt(parsedVal.days));
    var hoursCD = new ColumnDetail(parsedVal.hours, parseInt(parsedVal.hours), parseInt(parsedVal.hours));
    var minutesCD = new ColumnDetail(parsedVal.minutes, parseInt(parsedVal.minutes), parseInt(parsedVal.minutes));

    this.curPickerValue = new PickerSelection(daysCD, hoursCD, minutesCD);
  }

  private parseValue(value: string) {
    var days = value.substr(0, value.indexOf('.'));
    var hours = value.substr(value.indexOf('.') + 1, 2);
    var minutes = value.substr(value.lastIndexOf(':') + 1);

    if (days && hours && minutes 
      && !isNaN(parseInt(days)) 
      && !isNaN(parseInt(hours)) 
      && !isNaN(parseInt(minutes))) {
        return { days: days, hours: hours, minutes: minutes };
    }
    else
      return null;
  }

  private async openPicker() {    

    var options = this.getPickerOptions();
    var p = await this.pickerController.create(options);         
    
    await p.present();      

    var detail = await p.onDidDismiss();

    if (detail.role == 'done') {
      this.SetResult(detail.data);
      this.displayResult();
      this.validate(this.currentSelection);
      this.onChangeEvent.emit(new CustomEvent('onChange', { detail: this.currentSelection }));
    }
  }

  private validate(pickerS: PickerSelection)
  {
    var mp = this.minPickerValue;
    var ps = pickerS;

    if (mp) {
      if (!(ps.days > mp.days || (ps.days == mp.days && ps.hours > mp.hours) || 
      (ps.days == mp.days && ps.hours == mp.hours && ps.minutes > mp.minutes))) {
        this.formControl.setErrors({ 'CannotBeLowerThanMinValue': true });
      }
      else {
        
      }
    }
  }

  private SetResult(data: any)
  {
    var days = data['Days'];
    var hours = data['Hours'];
    var minutes = data['Minutes'];

    try {
      var daysCD = new ColumnDetail(days.text, days.value, days.columnIndex);
      var hoursCD = new ColumnDetail(hours.text, hours.value, hours.columnIndex);
      var minutesCD = new ColumnDetail(minutes.text, minutes.value, minutes.columnIndex);
      this.currentSelection = new PickerSelection(daysCD, hoursCD, minutesCD);      
    }
    catch(ex)
    {
      console.error('Internal error occured in the date time span picker');
    }
  }

  private displayResult()
  {
    var cs = this.currentSelection;
    var str = cs.days.text + ' Days, '  +  cs.hours.text + ' Hours, ' +  cs.minutes.text + ' Minutes';
    this.strDisplayLabel = str;
  }

  private getPickerOptions()
  {
    var options: PickerOptions = {} as PickerOptions;
    var columns: Array<PickerColumn> = new Array<PickerColumn>(3);
    var buttons: Array<PickerButton> = new Array<PickerButton>(2);

    options.columns = columns;
    options.buttons = buttons;

    columns[0] = {} as PickerColumn;
    columns[0].name = 'Days';
    columns[0].prefix = 'Days';
    columns[0].options = [];
    columns[0].selectedIndex = (this.currentSelection != null)? this.currentSelection.days.value : -1;        

    columns[1] = {} as PickerColumn;
    columns[1].name = 'Hours';
    columns[1].prefix = 'HH';
    columns[1].options = [];
    columns[1].selectedIndex = (this.currentSelection != null)? this.currentSelection.hours.value : -1;

    columns[2] = {} as PickerColumn;
    columns[2].name = 'Minutes';
    columns[2].prefix = 'mm';
    columns[2].options = [];
    columns[2].selectedIndex = (this.currentSelection != null)? this.currentSelection.minutes.value : -1;

    buttons[0] = {} as PickerButton;
    buttons[1] = {} as PickerButton;

    buttons[0].text = 'Cancel';
    buttons[0].role = 'cancel';

    buttons[1].text = 'Done';
    buttons[1].role = 'done';

    for(var i = 0; i < 7; i++) {
      var columnOption: PickerColumnOption = {};
      columnOption.text = i.toString();
      columnOption.value = i;
      columns[0].options.push(columnOption);
    }

    for(var i = 0; i < 24; i++) {
      var columnOption: PickerColumnOption = {};
      columnOption.text = i.toString();
      columnOption.value = i;
      columns[1].options.push(columnOption);
    }

    for(var i = 0; i < 60; i++) {
      var columnOption: PickerColumnOption = {};
      columnOption.text = i.toString();
      columnOption.value = i;
      columns[2].options.push(columnOption);
    }

    return options;
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
