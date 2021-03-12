import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/shared/cart/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {

  constructor(
    private activatedRouter: ActivatedRoute,
    private cartService: CartService) 
  { 
    activatedRouter.url.subscribe(dr => { console.log(dr) });
  }

  ngOnInit() 
  { }

  private increaseQty(itemId: number) {
    this.cartService.increaseQty(itemId);
  }

  private decreaseQty(itemId: number) {
    this.cartService.decreaseQty(itemId);
  }

  private removeItem(itemId: number) {
    this.cartService.removeItem(itemId);
  }
}
