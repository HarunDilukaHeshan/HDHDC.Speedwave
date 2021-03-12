import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/proxy/controllers';
import { OrderDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-myorders',
  templateUrl: './myorders.component.html',
  styleUrls: ['./myorders.component.scss'],
})
export class MyordersComponent implements OnInit {

  private dtoArr: Array<OrderDto> = [];

  constructor(
    private orderService: OrderService) 
  { }

  ngOnInit() 
  { 
    this.loadAllOrderDto();
  }

  private loadAllOrderDto() {
    this.orderService.getOrdersByTypeAndCityIdAndSkipCountAndMaxResultCount('history')
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
        console.log(dtoArr);
      });
  }
}
