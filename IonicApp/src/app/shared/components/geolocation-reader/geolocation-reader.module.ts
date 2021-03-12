import { AgmCoreModule } from '@agm/core';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { GeolocationReaderComponent } from './geolocation-reader.component';

@NgModule({
  declarations: [
    GeolocationReaderComponent
  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCOJQtymShiRPOUvbIoDYuToXBeCDVC5aA'
    }),
    CommonModule,
    ReactiveFormsModule,
    IonicModule,
  ],
  exports: [
    GeolocationReaderComponent
  ],
  providers: []
})
export class GeolocationReaderModule { }
