import { Component, OnInit, Input } from '@angular/core';
import { StoreChainDto } from 'src/app/proxy/dtos';
import { StoreChainService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-sc-item',
  templateUrl: './sc-item.component.html',
  styleUrls: ['./sc-item.component.scss'],
})
export class ScItemComponent implements OnInit {

  @Input('storeChain')
  private storeChain: StoreChainDto;

  private logoDataUri: string;

  constructor(
    private storeChainService: StoreChainService)
  { }

  ngOnInit() {
    this.loadLogo();
  }

  private loadLogo()
  {
    if (!this.storeChain) return;

    this.storeChainService.getLogoById(this.storeChain.id)
      .subscribe(data => {
        this.logoDataUri = data.base64DataUrl;
      }, err => { });
  }

}
