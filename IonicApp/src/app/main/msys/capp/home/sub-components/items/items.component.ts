import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemService } from 'src/app/proxy/controllers';
import { ItemDto } from 'src/app/proxy/dtos';
import { CartItem, CartService } from 'src/app/shared/cart/cart.service';

@Component({
  selector: 'app-home-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss'],
})
export class ItemsComponent implements OnInit {
  private maxResultCount: number = 3;
  private dtoArrArr: Array<Array<ItemDto>> = [];

  constructor(
    private cartService: CartService,
    private itemService: ItemService)
  { }

  private itemSlides = {
    slidesPerView: 3,
    initialSlide: 1,
    centerInsufficientSlides: true,
    centeredSlides: true
  };

  ngOnInit() {
    this.loadItemDto().subscribe();
    this.loadItemDto().subscribe();
    this.loadItemDto().subscribe();
    this.loadItemDto().subscribe();
  }

  private loadItemDto() {
    return new Observable<ItemDto[]>(ob => {
      this.itemService.getListByKeywordsAndSeedAndSkipCountAndMaxResultCountAndItemIdsArr(
        null, null, 0, this.maxResultCount, null)
        .subscribe(dtoArr => {
          this.dtoArrArr.push(dtoArr);
          ob.next(dtoArr);
        }, err => {
          ob.error(err);
        }, () => {
          ob.complete();
        });
    });
  }

  private addToCart(itemDto: ItemDto) {
    var cartItem = new CartItem(itemDto.id, itemDto.itemPrice, 1, itemDto.itemName);
    this.cartService.addItem(cartItem);
  }

  public onScrollReachEnd(ev: CustomEvent) {
    this.loadItemDto()
      .subscribe(dtoArr => { }, err => { }, () => { ev.target['complete'](); });
  }
}
