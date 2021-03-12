import { NgModule } from '@angular/core';
import { DatetimeSpanPicker2Component } from '../datetime-span-picker2/datetime-span-picker2.component';
import { DatetimeSpanPickerComponent } from './datetime-span-picker.component';

@NgModule({
  declarations: [
      DatetimeSpanPickerComponent,
      DatetimeSpanPicker2Component
  ],
  imports: [    
  ],
  exports: [
    DatetimeSpanPickerComponent,
    DatetimeSpanPicker2Component
  ],
  providers: []
})
export class DatetimeSpanPickerModule {}
