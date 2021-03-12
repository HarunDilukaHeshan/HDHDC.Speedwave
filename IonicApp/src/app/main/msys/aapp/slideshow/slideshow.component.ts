import { Component, OnInit } from '@angular/core';
import { SlideShowService } from 'src/app/proxy/controllers';
import { SlideShowPictureDto } from 'src/app/proxy/dtos';
import { GalleryReaderController } from 'src/app/shared/dependencies/gallery-reader-controller.injectable';

@Component({
  selector: 'app-slideshow',
  templateUrl: './slideshow.component.html',
  styleUrls: ['./slideshow.component.scss'],
})
export class SlideshowComponent implements OnInit {

  private dtoArr: Array<SlideShowPictureDto> = [];

  constructor(
    private slideShowService: SlideShowService) 
  { }

  ngOnInit() {    
    this.loadAllSlides();
  }

  private loadAllSlides() {
    this.slideShowService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      });
  }

  private delete(fileName: string) {    
    this.slideShowService.deleteByFileName(fileName)
      .subscribe(()=>{
        this.loadAllSlides();
      }, err => {});
  }
}
