export class AddCartItem {
    private readonly _itemId: number;
    private readonly _qty: number;

    public static readonly type = '[Shopping Cart] AddCartItem';
    public get itemId() { return this._itemId; }
    public get qty() { return this._qty; }

    constructor(itemId: number, qty: number) {
        this._itemId = itemId;
        this._qty = qty;
    }
}