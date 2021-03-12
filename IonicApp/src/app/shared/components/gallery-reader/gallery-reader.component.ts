import { Component, OnInit, ContentChild, ElementRef, Input, ViewChild, Injectable, Output, EventEmitter } from '@angular/core';
import { TriggerDirective } from './trigger.directive';
import { CameraResultType, CameraSource, Plugins } from '@capacitor/core';
import { MediaConstraints } from 'src/app/constraints/media-constriants';
import { Subject } from 'rxjs';
import { ImageResizer } from '../../dependencies/image-resizer.injectable';

const { Camera } = Plugins;

@Component({
  selector: 'app-gallery-reader',
  templateUrl: './gallery-reader.component.html',
  styleUrls: ['./gallery-reader.component.scss'],
  providers: [ImageResizer]
})
export class GalleryReaderComponent implements OnInit {

  @ContentChild('thumbnail')
  private imgElement: ElementRef;

  @ContentChild(TriggerDirective)
  private trigger: TriggerDirective;

  @Input('options')
  private options: Options = new Options();

  @Output('load')
  private loadEvent: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor(private imageResizer: ImageResizer) { }

  ngOnInit() { }

  ngAfterContentInit()
  {
    if (this.trigger === undefined || this.imgElement === undefined)
        throw Error('Trigger or thumbnail is not selected');
    this.addHandler(this.trigger.clickEventHandlers, this.trigger);
  }
  
  public setDataUri(dataUri: string, defaultBehavior: boolean = true)
  {
    this.imgElement.nativeElement.setAttribute('src', dataUri);
    if (defaultBehavior)
        this.loadEvent.emit(new CustomEvent('load', {detail: dataUri}));
  }

  private addHandler(subject: Subject<Event>, triggerD: TriggerDirective): void {
    subject.subscribe(async ev => {
      var data = await this.takePicture();
      this.imageResizer.resizeDataUrl(data, this.options.width, this.options.height,
        (resizedImgData: string) => {
          this.imgElement.nativeElement.setAttribute('src', resizedImgData);
          this.loadEvent.emit(new CustomEvent('load', {detail: resizedImgData}));
        });
    });
  }

  private async takePicture() {
    const image = await Camera.getPhoto({
      quality: 90,
      width: this.options.width,
      height: this.options.height,
      allowEditing: true,
      resultType: this.options.resultType,
      source: CameraSource.Photos,
      preserveAspectRatio: true
    });

    return image.dataUrl;
  }
}

class Options {
  width: number = MediaConstraints.categoryThumbnailWidth;
  height: number = MediaConstraints.categoryThumbnailHeight;
  resultType: CameraResultType = CameraResultType.DataUrl
}

