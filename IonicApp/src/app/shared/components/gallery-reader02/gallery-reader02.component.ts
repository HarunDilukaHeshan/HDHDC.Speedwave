import { AfterContentInit, Component, ContentChild, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CameraResultType, CameraSource, Plugins } from '@capacitor/core';
import { MediaConstraints } from 'src/app/constraints/media-constriants';

const { Camera } = Plugins;

@Component({
  selector: 'app-gallery-reader02',
  templateUrl: './gallery-reader02.component.html',
  styleUrls: ['./gallery-reader02.component.scss'],
})
export class GalleryReader02Component implements OnInit, AfterContentInit {

  // check to see if the thumbnail is an img element
  @ContentChild('thumbnail')
  private imgElem: ElementRef;

  @Input('options')
  private options: Options = new Options();

  @Output('onImgLoad')
  private onImgLoadEv: EventEmitter<string> = new EventEmitter<string>();

  constructor() 
  { }

  ngOnInit() 
  { }

  ngAfterContentInit() {
    
  }

  public async trigger() {
    var img = await this.takePicture();
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

    console.log(image);

    return image.dataUrl;
  }
}

class Options {
  width: number = MediaConstraints.categoryThumbnailWidth;
  height: number = MediaConstraints.categoryThumbnailHeight;
  resultType: CameraResultType = CameraResultType.DataUrl
}