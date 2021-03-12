import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { DayoftheweekComponent } from './dayoftheweek.component';

describe('DayoftheweekComponent', () => {
  let component: DayoftheweekComponent;
  let fixture: ComponentFixture<DayoftheweekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DayoftheweekComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(DayoftheweekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
