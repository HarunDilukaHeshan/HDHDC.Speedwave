import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/proxy/controllers';
import { OAuthService } from 'angular-oauth2-oidc';
import { ActivatedRoute } from '@angular/router';
import { CancelledOrderDto, OrderDto } from 'src/app/proxy/dtos';
import { OrderStatusConsts } from 'src/app/constants/order-status-consts';
import { ModalController } from '@ionic/angular';
import { CancelDialogComponent } from './cancel-dialog/cancel-dialog.component';
import { AvaiStoresComponent } from './avai-stores/avai-stores.component';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class DetailComponent implements OnInit {

  private isHaulted: boolean = false;
  private isSelected: boolean = false;
  private oStatus: string = 'd';
  private dto: OrderDto = null;

  constructor(
    private commonAlerts: CommonAlerts,
    private modalCtrl: ModalController,
    private activatedRoute: ActivatedRoute,
    private oAuthService: OAuthService,
    private orderService: OrderService)
  { }

  ngOnInit() {     
    this.init();
  }

  private async login() {
    await this.oAuthService.loadDiscoveryDocument();
    await this.oAuthService.fetchTokenUsingPasswordFlow('RiderThree', 'riderThree/03');
  }

  private async init() {
    await this.login();

    this.activatedRoute.params
      .subscribe(params => {
        var id = params['orderId'];
        if (id && !isNaN(id)) {
          this.loadOrderDto(id);
        }
      });
  }

  private async openCancelDialog() {
    var modal = await this.modalCtrl.create({
      component: CancelDialogComponent,
      showBackdrop: true,
      backdropDismiss: true,
      cssClass: 'cus-modal'
    });
    await modal.present();
    var data = await modal.onDidDismiss();
    if (data.role == 'done')
      this.cancelOrder({
        cancellationReasonId: data.data.crId,
        description: data.data.description,
        id: this.dto.id,
        orderDto: null,
        cancellationReasonDto: null
      });
  }

  private async openAvaiStoresDialog() {
    var modal = await this.modalCtrl.create({
      component: AvaiStoresComponent,
      showBackdrop: true,
      backdropDismiss: true,
      cssClass: 'cus-modal',
      componentProps: {
        cityId: (this.dto && this.dto.addressDto)? this.dto.addressDto.cityID : 0
      }
    });

    await modal.present();
  }
  
  private async rollbackOrder(id: number) {
    var result = await this.commonAlerts.warningAlert('',
      'Warning',
      'Do you want to perform this operation?',
      CommonAlertsButtonGroups.YesNo);

    if (result == CommonAlertsButtons.Yes)
      this.orderService.rollbackCancellationRequestById(id)
        .subscribe(dto => {
          console.log('Cancel request rollbacked');
          this.init();
        }, err => { });
  }

  private cancelOrder(dto: CancelledOrderDto) {
    this.orderService.sendCancellationRequestByDto(dto)
      .subscribe(() => {
        console.log('Cancel request sent');
        this.init();
      }, err => {

      });
  }

  private selectOrder(id: number) {
    this.orderService.selectOrderById(id)
      .subscribe(() => {
        console.log('Selected');        
        this.init();
      }, err => {

      });
  }

  private loadOrderDto(id: number) {
    this.orderService.getById(id)
      .subscribe(dto => {
        this.dto = dto;        
        this.oStatus = (dto.orderStatus.length > 0)? dto.orderStatus[0] : '';
        console.log(dto);
      }, err => {});
  }

}
