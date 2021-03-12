import { Component, OnInit } from '@angular/core';
import { QuantityDto } from 'src/app/proxy/dtos';
import { QuantityService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-quantities',
  templateUrl: './quantities.component.html',
  styleUrls: ['./quantities.component.scss'],
})
export class QuantitiesComponent implements OnInit {

  private quantityArr: Array<QuantityDto> = [];

  constructor(private quantityService: QuantityService) 
  { 
    this.loadAllQuantity();
  }

  ngOnInit() {}

  private loadAllQuantity()
  {
    this.quantityService.getAll()
      .subscribe(data => {
        this.quantityArr = data;
      }, err => {});
  }

  private ondelete()
  {
    this.loadAllQuantity();
  }
}
