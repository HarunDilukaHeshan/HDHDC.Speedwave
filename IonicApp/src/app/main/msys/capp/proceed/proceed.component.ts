import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { DeliveryScheduleService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-proceed',
  templateUrl: './proceed.component.html',
  styleUrls: ['./proceed.component.scss'],
})
export class ProceedComponent implements OnInit {

  constructor(
    private ds: DeliveryScheduleService,
    private oAuthService: OAuthService) 
  { 
    oAuthService.loadDiscoveryDocument();
    ds.getCompatiblesByAddressIdAndItemIds(4, [4, 6])
    .subscribe();
  }

  ngOnInit() 
  { 

  }

}
