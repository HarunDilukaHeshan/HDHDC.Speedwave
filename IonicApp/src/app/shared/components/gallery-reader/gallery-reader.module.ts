import { NgModule } from '@angular/core';
import { GalleryReaderComponent } from './gallery-reader.component';
import { TriggerDirective } from './trigger.directive';

@NgModule({
  declarations: [
      GalleryReaderComponent,
      TriggerDirective
  ],
  imports: [    
  ],
  exports: [
    GalleryReaderComponent,
    TriggerDirective
  ],
  providers: []
})
export class GalleryReaderModule {}
