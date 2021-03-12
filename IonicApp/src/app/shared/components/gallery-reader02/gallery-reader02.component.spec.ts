import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { GalleryReader02Component } from './gallery-reader02.component';

describe('GalleryReader02Component', () => {
  let component: GalleryReader02Component;
  let fixture: ComponentFixture<GalleryReader02Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GalleryReader02Component ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(GalleryReader02Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
