import { Injectable } from "@angular/core";
import { Store, Select } from '@ngxs/store';
import { ReplaySubject, Subject } from 'rxjs';

import { first } from 'rxjs/operators';
import { ItemService } from 'src/app/proxy/controllers';
import { ItemDto } from 'src/app/proxy/dtos';
import { AddCartItem } from 'src/app/state-management/actions/AddCartItem.action';
import { CleanTheCart } from 'src/app/state-management/actions/CleanTheCart.action';
import { RemoveCartItem } from 'src/app/state-management/actions/RemoveCartItem.action';
import { CartStateModel } from 'src/app/state-management/state-containers/cart.state';

@Injectable({ providedIn: 'root' })
export class CartService {

    private _cartItemArr: Array<CartItem> = [];    
    private _subject: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);

    constructor(
        private itemService: ItemService,
        private store: Store) {                   
        this.init();
    }

    private async init() {

        var cartState = this.store.select<CartStateModel>(state => state.cartState);
        var pipe = cartState.pipe(first());

        pipe.subscribe(async state => {
            
            if (state.cartItemArr.length > 0) {
                var arr = state.cartItemArr.map(val => val.itemId);
                var dtoArr = await this.loadCartItems(arr);
                dtoArr.forEach(val => {
                    var item = state.cartItemArr.find(v => v.itemId == val.id);
                    if (!item) throw Error('Item not found');
                    this.add(new CartItem(item.itemId, val.itemPrice, item.qty, val.itemName));
                });                
            }
            this._subject.next(true);            
        }, err => {
            this._subject.error(err);
        });
    }

    private loadCartItems(itemIdsArr: number[]) {
        return new Promise<ItemDto[]>((resolve, reject) => {
            this.itemService.getListByKeywordsAndSeedAndSkipCountAndMaxResultCountAndItemIdsArr(
                null, null, null, null, itemIdsArr)
                .subscribe(dtoArr => {
                    resolve(dtoArr);                    
                }, err => { reject(err); }, () => { reject(); });
        });
    }

    private remove(itemId: number): number {
        var i = this._cartItemArr.findIndex(val => val.itemId === itemId);
        if (i == -1) throw Error('invalid item id');

        this._cartItemArr.splice(i, 1);
        return i;
    }

    private add(cartItem: CartItem) {
        var cItem = this._cartItemArr.find(val => val.itemId === cartItem.itemId);
        if (cItem) {
            var i = this.remove(cItem.itemId);
            this._cartItemArr.splice(i, 0,
                new CartItem(cItem.itemId, cItem.itemPrice, cItem.qty + cartItem.qty, cItem.itemName));
        }
        else this._cartItemArr.push(cartItem);
    }

    public onReady() {
        var pipe = this._subject.pipe();
        return pipe;
    }

    public increaseQty(itemId: number) {
        var cItem = this._cartItemArr.find(val => val.itemId === itemId);
        if (!cItem) throw Error('invalid item id');

        this.addItem(new CartItem(itemId, cItem.itemPrice, 1, cItem.itemName));
    }

    public decreaseQty(itemId: number) {
        var cItem = this._cartItemArr.find(val => val.itemId === itemId);
        if (!cItem) throw Error('invalid item id');

        if (cItem.qty - 1 > 0) {
            var i = this.remove(itemId);
            this._cartItemArr.splice(i, 0, 
                new CartItem(itemId, cItem.itemPrice, cItem.qty - 1, cItem.itemName));
            this.store.dispatch(new RemoveCartItem(itemId, 1));
        }
    }

    public addItem(cartItem: CartItem): void {        
        this.add(cartItem);
        this.store.dispatch(new AddCartItem(cartItem.itemId, cartItem.qty));
    }

    public removeItem(itemId: number): void {
        console.log('remove item');
        this.store.dispatch(new RemoveCartItem(itemId, 0));
        this.remove(itemId);
    }

    public clean(): void {
        this.store.dispatch(new CleanTheCart());
        this._cartItemArr = [];
    }

    public get items() { 
        return Array.from(this._cartItemArr); 
    }

    public get subtotal() {
        var subtotal = 0;
        this._cartItemArr.forEach(val => { subtotal += val.itemPrice * val.qty });        
        return subtotal;
    }

    public get qty() {
        var qty = 0;
        this._cartItemArr.forEach(val => { qty += val.qty });
        return qty;
    }
}

export class CartItem {
    private readonly _itemId: number;
    private readonly _itemPrice: number;
    private readonly _itemName: string;
    private readonly _qty: number;

    constructor(itemId: number, itemPrice: number, qty: number, itemName: string) {
        this._itemId = itemId;
        this._itemPrice = itemPrice;
        this._qty = (qty > 0)? qty : 1;
        this._itemName = itemName;
    }

    public get itemId(): number { return this._itemId; }
    public get itemPrice(): number { return this._itemPrice; }
    public get itemName(): string { return this._itemName; }
    public get qty(): number { return this._qty; }
}