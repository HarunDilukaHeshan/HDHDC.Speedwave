import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { ItemService } from 'src/app/proxy/controllers';
import { ItemDto } from 'src/app/proxy/dtos';
import { CartItem, CartService } from 'src/app/shared/cart/cart.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {

  private dtoArr: Array<ItemDto> = [];

  private keywords: string = '';
  private skipCount: number = 0;
  private maxResultCount: number = 10;

  constructor(
    private cartService: CartService,
    private activatedRoute: ActivatedRoute,
    private itemService: ItemService) 
  { 
    this.init();
  }

  ngOnInit() { 
    this.itemService.search(this.keywords, this.skipCount, this.maxResultCount)
      .subscribe(dtoArr => { this.dtoArr = dtoArr; });
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var keywords = params['keywords'];
        if (keywords) this.keywords = keywords;
      });
  }

  private onSearchChange(ev: CustomEvent<{ value: string }>) {
    this.keywords = ev.detail.value;
    this.skipCount = 0;

    this.itemService.search(this.keywords, this.skipCount, this.maxResultCount)
      .subscribe(dtoArr => { 
        this.dtoArr = dtoArr; 
      });
  }

  private onScrollReachEnd(ev: CustomEvent) {
    this.itemService.search(this.keywords, this.skipCount, this.maxResultCount)
      .subscribe(dtoArr => {
        this.dtoArr = this.dtoArr.concat(dtoArr);
        this.skipCount += this.maxResultCount;        
        ev.target['complete']();
      }, err => { 
        ev.target['complete']();
      }, () => { ev.target['complete'](); });
  }

  private addToCart(itemDto: ItemDto) {
    var cartItem = new CartItem(itemDto.id, itemDto.itemPrice, 1, itemDto.itemName);
    this.cartService.addItem(cartItem);
  }
}
