import { Injectable } from "@angular/core";
import { MediaConstraints } from 'src/app/constraints/media-constriants';
import { CameraResultType, CameraSource, Plugins } from '@capacitor/core';
import { ImageResizer } from './image-resizer.injectable';

const { Camera } = Plugins;

@Injectable({
    providedIn: 'root'    
})
export class GalleryReaderController {

    constructor (
        private imageResizer: ImageResizer)
    { }

    public async create(options: Options) {
        options.width = (options.width)? options.width : MediaConstraints.itemThumbnailWidth
        options.height = (options.height)? options.width : MediaConstraints.itemThumbnailWidth
        options.resultType = (options.resultType)? options.resultType : CameraResultType.DataUrl;

        return new GalleryReaderElement(options, this.imageResizer);
    }
}

// Options class
export interface Options {
    width?: number;
    height?: number;
    resultType?: CameraResultType;
}

// Image data holder
class ImgDataUri {    
    constructor(private _dataUri: string, private _format: string) 
    { }
    
    get dataUri() { return this._dataUri; }
    get format() { return this._format; }
}

// GalleryReaderElement
export class GalleryReaderElement {
    constructor(
        private options: Options, 
        private imageResizer: ImageResizer) 
    { }

    public async present() {
        const image = await Camera.getPhoto({
            quality: 90,
            width: this.options.width,
            height: this.options.height,
            allowEditing: true,
            resultType: this.options.resultType,
            source: CameraSource.Photos,
            preserveAspectRatio: true
          });         
          
        var dataUri = await this.imageResizer.resizeDataUriAsync(
            image.dataUrl, 
            this.options.width, 
            this.options.height);
          
        return new ImgDataUri(dataUri, image.format);
    }
}