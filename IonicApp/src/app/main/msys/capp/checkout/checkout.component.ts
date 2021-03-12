import { Component, Injectable, OnInit } from '@angular/core';
import { CustomerAccountService } from 'src/app/proxy/account-controllers';
import { DeliveryScheduleService, OrderService } from 'src/app/proxy/controllers';
import { OAuthService } from 'angular-oauth2-oidc';
import { AddressDto, CartItemDto, DeliveryChargeDto, DeliveryScheduleDto, OrderCreateDto } from 'src/app/proxy/dtos';
import { Store, State, Action, StateContext, Select } from '@ngxs/store';
import { CartService } from 'src/app/shared/cart/cart.service';
import { asyncScheduler, BehaviorSubject, concat, ReplaySubject, Subject } from 'rxjs';
import { buffer, bufferCount, observeOn, subscribeOn, takeLast } from 'rxjs/operators';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
  providers: []
})
export class CheckoutComponent implements OnInit {

  private dsDtoArr: Array<DeliveryScheduleDto> = [];
  private addrDtoArr: Array<AddressDto> = [];
  private addrId: number = 0;
  private dsId: number = 0;
  private isWaiting: boolean = false;
  private dcDto: DeliveryChargeDto = null;

  constructor(
    private orderService: OrderService,
    private cartService: CartService,
    private customerService: CustomerAccountService,
    private deliveryScheduleService: DeliveryScheduleService) 
  {
    
    this.cartService.onReady()
      .subscribe(() => {
        var rr = cartService.items.map(val => val.itemId);

      });    
  }

  ngOnInit() {
    this.init();
  }

  private async init() {    
    this.loadAllAddress();
  }

  private onAddrChange(ev: CustomEvent<{ value: number }>) {    
    this.cartService.onReady()
      .subscribe(() => {
        this.addrId = ev.detail.value;
        var itemIdsArr = this.cartService.items.map(val => val.itemId);
        this.loadCompDeliverySchedule(this.addrId, itemIdsArr);
      });        
  }

  private onDsChange(ev: CustomEvent<{ value: number }>) {
    this.dsId = ev.detail.value;    
    this.cartService.onReady().subscribe(isReady => {
      var addrId = this.addrId;
      var dsId = ev.detail.value;      
      var items = this.cartService.items.map(v => { return { itemID: v.itemId, qty: v.qty } as CartItemDto });
      
      this.orderService.calculateDeliveryChargeByAddressIdAndDeliveryScheduleIdAndItemsArr(addrId, dsId, items)
        .subscribe(dto => {
          this.dcDto = dto;
        }, err => {

        });
    });    
  }

  private loadAllAddress() {
    return new Promise((resolve, reject) => {
      this.customerService.getAllAddress()
        .subscribe((dtoArr) => {
          this.addrDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => { reject(err); }, () => { reject(); });
    });
  }

  private loadCompDeliverySchedule(addrId: number, itemIds: number[]) {
    return new Promise((resolve, reject) => {
      this.deliveryScheduleService.getCompatiblesByAddressIdAndItemIds(addrId, itemIds)
        .subscribe(dtoArr => {
          this.dsDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => { reject(err); }, () => { reject(); })
    });
  }

  private createOrder() {
    var dto = this.getOrderCreateDto();
    this.isWaiting = true;
    this.orderService.createByOrderCreateDto(dto)
      .subscribe(dto => {        
        this.cartService.clean();
        this.dcDto = null;
        this.isWaiting = false;
      }, err => {
        this.isWaiting = false;
      });
  }

  private isValid() {
    return this.addrId > 0 && this.dsId > 0 && this.cartService.items.length > 0;
  }

  private getOrderCreateDto() {
    var dto = { } as OrderCreateDto;

    dto.addressID = this.addrId;
    dto.deliveryScheduleID = this.dsId;
    dto.items = this.cartService.items.map(v => { return { itemID: v.itemId, qty: v.qty } as CartItemDto });

    return dto;
  }
}

