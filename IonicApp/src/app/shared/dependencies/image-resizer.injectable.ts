import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'any' })
export class ImageResizer
{
  public resizeBase64(
    base64: string, 
    width: number, 
    height: number, 
    callback: (data: string)=>void): void
  {
    var img = this.getImgObject(this.base64ToDataUri(base64), width, height);
    img.onload = () => { 
        var resizedDataUri = this.resizeImage(img, width, height); 
        callback(resizedDataUri);
    };
  }

  public resizeDataUrl(
    dataUri: string, 
    width: number, 
    height: number, 
    callback: (data: string)=>void): void
  {
    var img = this.getImgObject(dataUri, width, height);
    img.onload = () => { 
        var resizedDataUri = this.resizeImage(img, width, height); 
        callback(resizedDataUri);
    };
  }  

  public resizeDataUriAsync(dataUri: string, width: number, height: number) {
    return new Promise<string>((resolve, reject) => {
      var imgElem = this.getImgObject(dataUri, width, height);
      imgElem.onload = () => { resolve(this.resizeImage(imgElem, width, height)); };
      imgElem.onerror = () => { reject(); };
    });
  }

  private getImgObject(
    dataUri: string, 
    width: number, 
    height: number): HTMLImageElement
  {
    var img = new Image(width, height);
    img.src = dataUri;
    return img;
  }

  private base64ToDataUri(base64Str: string)
  {
    return 'data:image/jpeg;base64,' + base64Str;
  }

  private resizeImage(
    img: HTMLImageElement, 
    width: number, 
    height: number): string
  {
    var canvas =  document.createElement('canvas');
    var ctx = canvas.getContext('2d');

    canvas.width = width;
    canvas.height = height;
    
    ctx.drawImage(img, 0, 0, width, height);

    return canvas.toDataURL('image/jpeg');
  }
}
