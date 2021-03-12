import { Component, OnInit } from '@angular/core';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';
import { ItemDto } from 'src/app/proxy/dtos';
import { ItemService } from 'src/app/proxy/controllers';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss'],
})
export class ItemsComponent implements OnInit {

  private itemDtoArr: Array<ItemDto> = [];

  constructor(
    private commonAlerts: CommonAlerts, 
    private itemService: ItemService) 
  { 
    this.loadAllItemDto();
  }

  ngOnInit() {    
  }   

  private loadAllItemDto()
  {
    this.itemService.search()
      .subscribe(data => {
        this.itemDtoArr = data;
      }, err => {
        
      });
  }
}
