import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { MrtItemComponent } from './mrt-item.component';

describe('MrtItemComponent', () => {
  let component: MrtItemComponent;
  let fixture: ComponentFixture<MrtItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MrtItemComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(MrtItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
