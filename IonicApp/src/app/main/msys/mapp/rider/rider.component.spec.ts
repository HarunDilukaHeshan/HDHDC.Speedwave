import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { RiderComponent } from './rider.component';

describe('RiderComponent', () => {
  let component: RiderComponent;
  let fixture: ComponentFixture<RiderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RiderComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(RiderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
