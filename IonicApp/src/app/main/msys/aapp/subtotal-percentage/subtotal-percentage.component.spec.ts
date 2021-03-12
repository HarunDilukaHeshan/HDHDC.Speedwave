import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { SubtotalPercentageComponent } from './subtotal-percentage.component';

describe('SubtotalPercentageComponent', () => {
  let component: SubtotalPercentageComponent;
  let fixture: ComponentFixture<SubtotalPercentageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubtotalPercentageComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(SubtotalPercentageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
