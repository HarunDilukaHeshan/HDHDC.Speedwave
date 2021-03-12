import { Injectable } from '@angular/core';
import { State, Store, Action, StateContext } from '@ngxs/store';

import { AddCartItem } from '../actions/AddCartItem.action';
import { CleanTheCart } from '../actions/CleanTheCart.action';
import { RemoveCartItem } from '../actions/RemoveCartItem.action';

export interface CartStateModel {
    cartItemArr: Array<CItem>;
}

export interface CItem {
    itemId: number;
    qty: number;
}

@State<CartStateModel>({
    name: 'cartState',
    defaults: {
        cartItemArr: []
    }
})
@Injectable()
export class CartState {

    @Action(AddCartItem)
    addCartItem(ctx: StateContext<CartStateModel>, action: AddCartItem) {
        var state = ctx.getState();
        var newItem: CItem = { itemId: action.itemId, qty: action.qty };
        var ci = state.cartItemArr.find(val => val.itemId == action.itemId);
        var newCartItemArr: Array<CItem> = [];

        if (ci) newItem.qty = ci.qty + action.qty;

        state.cartItemArr.forEach(val => {
            if (ci == undefined || ci.itemId != val.itemId)
                newCartItemArr.push({ itemId: val.itemId, qty: val.qty });
        });

        ctx.setState({ ...state, cartItemArr: [...newCartItemArr, newItem] });
    }

    @Action(RemoveCartItem)
    removeCartItem(ctx: StateContext<CartStateModel>, action: RemoveCartItem) {
        var state = ctx.getState();
        var ci = state.cartItemArr.find(val => val.itemId == action.itemId);
        var newCartItemArr: Array<CItem> = [];

        if (!ci) return;

        if (ci.qty < 0) throw Error('Negative qty is invalid');

        var reQty = ci.qty - action.qty;

        state.cartItemArr.forEach(val => {
            if (reQty > 0 && ci.itemId == val.itemId && action.qty > 0)
                newCartItemArr.push({ itemId: val.itemId, qty: reQty });
            else if (ci.itemId != val.itemId)
                newCartItemArr.push({ itemId: val.itemId, qty: val.qty });
        });

        ctx.setState({ ...state, cartItemArr: [ ...newCartItemArr ] });
    }

    @Action(CleanTheCart)
    cleanTheCart(ctx: StateContext<CartStateModel>) {
        var state = ctx.getState();
        ctx.setState({ cartItemArr: [] });
    }
}