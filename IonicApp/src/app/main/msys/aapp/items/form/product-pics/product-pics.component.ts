import { Component, OnInit, Input, ViewChild, ViewChildren, QueryList, ElementRef, EventEmitter, Output } from '@angular/core';
import { GalleryReaderComponent } from 'src/app/shared/components/gallery-reader/gallery-reader.component';
import { ItemService } from 'src/app/proxy/controllers';
import { BlobFileDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-product-pics',
  templateUrl: './product-pics.component.html',
  styleUrls: ['./product-pics.component.scss'],
})
export class ProductPicsComponent implements OnInit {  
  private imgArr: Array<ImgDataUri> = [];
  private selectedThumb: ImgDataUri;
  private selectedImg: ImgDataUri; 
  
  private addedPics: Array<ImgDataUri> = [];
  private removedPics: Array<ImgDataUri> = [];
  private thumbPic: ImgDataUri = null;

  @Output('onThumbChange')
  private onThumbChangeEv: EventEmitter<CustomDetail> = new EventEmitter<CustomDetail>();

  @Output('onImgAdd')
  private onImgAddEv: EventEmitter<CustomDetail> = new EventEmitter<CustomDetail>();

  @Output('onImgRemove')
  private onImgRemoveEv: EventEmitter<CustomDetail> = new EventEmitter<CustomDetail>();

  @Output('onReset')
  private onResetEv: EventEmitter<CustomDetail> = new EventEmitter<CustomDetail>();

  constructor() 
  { }

  ngOnInit() 
  { }

  public reset(imgArr: Array<ImgDataUri>, thumb: ImgDataUri) {
    if (imgArr.find(val => val === thumb) === undefined) throw Error('thumb nail does not exists in the imgArr');
    this.imgArr = imgArr;
    this.selectedThumb = thumb;
    this.selectedImg = thumb;
    this.onResetEv.emit(new CustomDetail(this.thumbPic, this.addedPics, this.removedPics));
  }

  public addImg(img: ImgDataUri) {
    this.imgArr.push(img);
    this.selectImg(img);
    if (this.imgArr.length == 1) this.setAsThumb(img);
    this.addedPics.push(img);
    this.onImgAddEv.emit(new CustomDetail(this.thumbPic, this.addedPics, this.removedPics));
  }

  private selectImg(img: ImgDataUri) {
    this.selectedImg = img;
  }

  private removeImg(img: ImgDataUri) {
    var indx = this.imgArr.findIndex(val => val === img);
    this.imgArr.splice(indx, 1);    
    if (this.selectedThumb === img && this.imgArr.length > 0) this.setAsThumb(this.imgArr[0]);
    if (this.imgArr.length > 0) 
      this.selectImg(this.imgArr[0]);
    else {
      this.selectedImg = null;
      this.selectedThumb = null;
    }

    indx = this.addedPics.indexOf(img);    
    if (indx > -1) this.addedPics.splice(indx, 1);           
    
    if (img.fileName !== '') {
      this.removedPics.push(img);
      this.onImgRemoveEv.emit(new CustomDetail(this.thumbPic, this.addedPics, this.removedPics));
    }
  }

  private setAsThumb(img: ImgDataUri) {
    this.selectedThumb = img;
    this.thumbPic = img;
    this.onThumbChangeEv.emit(new CustomDetail(this.thumbPic, this.addedPics, this.removedPics));
  }
}

export class ImgDataUri {

  private readonly _dataUri: string;
  private readonly _fileName: string;

  constructor(dataUri: string, filename: string = '')
  {
    this._dataUri = dataUri;
    this._fileName = filename;
  }

  public get dataUri() { return this._dataUri; }
  public get fileName() { return this._fileName; }
}

export class CustomDetail {
  private _removedImgDataUriArr: Array<ImgDataUri> = [];
  private _addedImgDataUriArr: Array<ImgDataUri> = [];
  private _thumbnailImgDataUri: ImgDataUri;

  constructor(
    thumbnail: ImgDataUri,
    addedImgArr: Array<ImgDataUri>,
    removedImgArr: Array<ImgDataUri>) {
    if (!(thumbnail || addedImgArr || removedImgArr)) throw Error('Invalid argument exception');
    this._thumbnailImgDataUri = (thumbnail)? new ImgDataUri(thumbnail.dataUri, thumbnail.fileName) : null;
    this._addedImgDataUriArr = this.cloneArray(addedImgArr);
    this._removedImgDataUriArr = this.cloneArray(removedImgArr);
  }

  get addedImgDataUriArr() {
    return this.cloneArray(this._addedImgDataUriArr);
  }

  get removedImgDataUriArr() {
    return this.cloneArray(this._removedImgDataUriArr);
  }

  get thumbnailImgDataUri() {
    if (this._thumbnailImgDataUri)
      return new ImgDataUri(this._thumbnailImgDataUri.dataUri, this._thumbnailImgDataUri.fileName);
    return null;
  }

  private cloneArray(arr: Array<ImgDataUri>): Array<ImgDataUri> {
    var newArr = [];
    arr.forEach(val => { newArr.push(new ImgDataUri(val.dataUri, val.fileName)) });
    return newArr;
  }
}