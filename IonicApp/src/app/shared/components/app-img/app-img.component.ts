import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { ItemService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-img',
  templateUrl: './app-img.component.html',
  styleUrls: ['./app-img.component.scss'],
})
export class AppImgComponent implements OnChanges, OnInit {

  private imgDataUri: string = '';

  @Input('src')
  private src: string = '';

  constructor(
    private itemService: ItemService) 
  { }

  ngOnChanges() {
    this.loadDataUri();
  }

  ngOnInit() 
  { }

  private loadDataUri() {
    this.itemService.getThumbnailByItempic(this.src)
      .subscribe(dto => {
        this.imgDataUri = dto.base64DataUrl;
      }, err => { });
  }
}
