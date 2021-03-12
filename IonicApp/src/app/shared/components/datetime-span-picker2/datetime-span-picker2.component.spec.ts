import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { DatetimeSpanPicker2Component } from './datetime-span-picker2.component';

describe('DatetimeSpanPicker2Component', () => {
  let component: DatetimeSpanPicker2Component;
  let fixture: ComponentFixture<DatetimeSpanPicker2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatetimeSpanPicker2Component ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(DatetimeSpanPicker2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
