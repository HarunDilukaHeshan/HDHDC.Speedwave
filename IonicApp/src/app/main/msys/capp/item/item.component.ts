import { Component, ContentChild, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CameraResultType } from '@capacitor/core';
import { ItemService } from 'src/app/proxy/controllers';
import { BlobFileDto, ItemDto } from 'src/app/proxy/dtos';
import { CartItem, CartService } from 'src/app/shared/cart/cart.service';
import { GalleryReaderController } from 'src/app/shared/dependencies/gallery-reader-controller.injectable';
import { ImgDataUri } from '../../aapp/items/form/product-pics/product-pics.component';
import { ThumbComponent } from './thumb/thumb.component';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss'],
})
export class ItemComponent implements OnInit {

  private itemDto: ItemDto;
  private itemPicDtoArr: Array<BlobFileDto> = [];

  @ViewChild('thumb') 
  private thumb: ThumbComponent;

  constructor(
    private cartService: CartService,
    private gr: GalleryReaderController,
    private activatedRoute: ActivatedRoute,
    private itemService: ItemService) 
  { }

  ngOnInit() {
    this.init();    
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['itemId'];
        if (id && !isNaN(id)) {
          this.loadItemDto(id);
        }
      });
  }

  private loadItemDto(id: number) {
    this.itemService.getById(id)
      .subscribe(itemDto => {
        this.itemDto = itemDto;
        this.loadItemPictures(id);
      }, err => { });
  }

  private loadItemPictures(id: number) {
    this.itemService.getAllItemPicById(id)
      .subscribe(dtoArr => {
        this.itemPicDtoArr = dtoArr;
      }, err => {});
  }

  private addToCart(itemDto: ItemDto) {
    var cartItem = new CartItem(itemDto.id, itemDto.itemPrice, 1, itemDto.itemName);
    this.cartService.addItem(cartItem);
  }
}
