import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { AppImgComponent } from './app-img.component';

describe('AppImgComponent', () => {
  let component: AppImgComponent;
  let fixture: ComponentFixture<AppImgComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppImgComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(AppImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
