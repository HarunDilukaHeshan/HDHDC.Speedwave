import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ImgDataUri } from '../../../aapp/items/form/product-pics/product-pics.component';

@Component({
  selector: 'app-thumb',
  templateUrl: './thumb.component.html',
  styleUrls: ['./thumb.component.scss'],
})
export class ThumbComponent implements OnInit {

  private imgArr: Array<ImgDataUri> = [];
  private selectedThumb: ImgDataUri;
  private selectedImg: ImgDataUri;

  @Output('onThumbChange')
  private onThumbChangeEv: EventEmitter<void> = new EventEmitter<void>();

  @Output('onImgAdd')
  private onImgAddEv: EventEmitter<void> = new EventEmitter<void>();

  @Output('onImgRemove')
  private onImgRemoveEv: EventEmitter<void> = new EventEmitter<void>();

  @Output('onReset')
  private onResetEv: EventEmitter<void> = new EventEmitter<void>();

  constructor() 
  { }

  ngOnInit() 
  { }

  public reset(imgArr: Array<ImgDataUri>, thumb: ImgDataUri) {
    if (imgArr.find(val => val === thumb) === undefined) throw Error('thumb nail does not exists in the imgArr');
    this.imgArr = imgArr;
    this.selectedThumb = thumb;
  }

  public addImg(img: ImgDataUri) {
    this.imgArr.push(img);
    this.selectImg(img);
    if (this.imgArr.length == 1) this.setAsThumb(img);
    this.onImgAddEv.emit();
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
  }

  private setAsThumb(img: ImgDataUri) {
    this.selectedThumb = img;
  }
}
