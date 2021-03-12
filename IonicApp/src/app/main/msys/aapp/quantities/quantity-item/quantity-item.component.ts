import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { QuantityDto } from 'src/app/proxy/dtos';
import { QuantityService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-quantity-item',
  templateUrl: './quantity-item.component.html',
  styleUrls: ['./quantity-item.component.scss'],
})
export class QuantityItemComponent implements OnInit {

  @Input('dto')
  private quantityDto: QuantityDto;

  @Output('ondelete')
  private ondelete: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor(private quantityService: QuantityService) 
  { }

  ngOnInit() {}

  private delete() {
    this.quantityService.deleteById(this.quantityDto.id)
      .subscribe(() => {
        this.ondelete.emit(new CustomEvent('ondelete', { detail: { dto: this.quantityDto } }));
      }, err => { });
  }
}
