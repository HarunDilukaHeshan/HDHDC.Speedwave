import { Component, OnInit } from '@angular/core';
import { StoreChainService } from 'src/app/proxy/controllers';
import { StoreChainDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-store-chain',
  templateUrl: './store-chain.component.html',
  styleUrls: ['./store-chain.component.scss'],
})
export class StoreChainComponent implements OnInit {

  private storeChainDtoArr: Array<StoreChainDto> = [];

  constructor(
    private storeChainService: StoreChainService) 
  { }

  ngOnInit() {
    this.loadAllStoreChainDto();
  }

  private loadAllStoreChainDto()
  {
    this.storeChainService.getAll()
      .subscribe(data => {
        this.storeChainDtoArr = data;   
        console.log(data);     
      }, err => {});
  }

}
