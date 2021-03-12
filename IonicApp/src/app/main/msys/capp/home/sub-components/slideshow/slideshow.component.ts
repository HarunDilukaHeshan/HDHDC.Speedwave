import { Component, OnDestroy, OnInit } from '@angular/core';
import { UrlSegment, UrlTree } from '@angular/router';
import { NavController } from '@ionic/angular';
import { Subscription } from 'rxjs';
import { SlideShowService } from 'src/app/proxy/controllers';
import { SlideShowPictureDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-home-slideshow',
  templateUrl: './slideshow.component.html',
  styleUrls: ['./slideshow.component.scss'],
})
export class SlideshowComponent implements OnInit, OnDestroy {

  private dtoArr: Array<SlideShowPictureDto> = [];
  private subArr: Array<Subscription> = [];

  constructor(
    private navCtrl: NavController,
    private slideShowService: SlideShowService) 
  { }

  ngOnInit() {
    this.loadAllSlides();
  }

  private loadAllSlides() {
    var subs = this.slideShowService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => { });

      this.subArr.push(subs);
  }

  private navigateToLink(fileName: string) {
    var slide = this.dtoArr.find(dto => dto.fileName === fileName);
    if (!slide) throw Error('Invalid fileName');

    if (!slide.uri || slide.uri == '') return;

    var url = slide.uri;
    var pattern1 = /keywords=[\w ]+/i
    var pattern2 = /\/\d+/i;

    var match1 = url.match(pattern1);
    var match2 = url.match(pattern2);

    if (match2) {
      var value = match2[0].substr(1);
      this.navCtrl.navigateForward(['msys/capp/item', { itemId: value }]);
    }
    else if (match1) {      
      var value = match1[0].substr(match1[0].indexOf('=') + 1);
      this.navCtrl.navigateForward(['msys/capp/search', { keywords: value }]);
    }
  }

  ngOnDestroy() {
    this.subArr.forEach(sub => { sub.unsubscribe(); });
  }
}
