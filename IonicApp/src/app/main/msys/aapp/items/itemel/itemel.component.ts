import { Component, OnInit, Input } from '@angular/core';
import { ItemDto } from 'src/app/proxy/dtos';
import { ItemService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-itemel',
  templateUrl: './itemel.component.html',
  styleUrls: ['./itemel.component.scss'],
})
export class ItemelComponent implements OnInit {

  @Input('itemdto')
  private itemDto: ItemDto;

  private thumbnailDataUri: string = '';

  constructor(private itemService: ItemService) 
  { }

  ngOnInit() {
    this.loadThumbnail();
  }

  private loadThumbnail()
  {
    this.itemService.getThumbnailByItempic(this.itemDto.itemThumbnail)
      .subscribe(data => {
        if (data && data.base64DataUrl != "") {
          this.thumbnailDataUri = data.base64DataUrl;
      } 
      else {
        this.thumbnailDataUri = "assets/item.jpg";
      }
      }, err => {});
  }

}
