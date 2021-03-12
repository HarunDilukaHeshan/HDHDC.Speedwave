import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UnitDto } from 'src/app/proxy/dtos';
import { UnitService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-units-item',
  templateUrl: './units-item.component.html',
  styleUrls: ['./units-item.component.scss'],
})
export class UnitsItemComponent implements OnInit {

  @Input('unitdto')
  private unitdto: UnitDto;

  @Output('ondelete')
  private deleteEvent: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor(
    private unitService: UnitService) 
  { }

  ngOnInit() 
  { }

  private async deleteUnit()
  {
    console.log(this.unitdto);
    this.unitService.deleteById(this.unitdto.id)
      .subscribe(() => {
        alert('deleted');
        this.deleteEvent.emit(new CustomEvent('ondelete', { detail: { id: this.unitdto.id }}));
      }, err => {

      });
  }
}
