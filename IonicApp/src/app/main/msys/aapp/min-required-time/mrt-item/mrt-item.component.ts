import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MinRequiredTimeDto } from 'src/app/proxy/dtos';
import { MinRequiredTimeService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-mrt-item',
  templateUrl: './mrt-item.component.html',
  styleUrls: ['./mrt-item.component.scss'],
})
export class MrtItemComponent implements OnInit {

  private awaiting: boolean = false;

  @Input('dto')
  private dto: MinRequiredTimeDto;

  @Output('ondelete')
  private deleteEvent: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  constructor(private mrtService: MinRequiredTimeService) 
  { }

  ngOnInit() {}

  private deleteDto()
  {
    if (this.awaiting) return;
    this.awaiting = true;
    this.mrtService.deleteById(this.dto.id)
      .subscribe(()=>{
        this.awaiting = false;
        this.deleteEvent.emit(new CustomEvent('ondelete', { detail: { dto: this.dto } }));
      }, err=>{
        this.awaiting = false;
      });
  }

}
