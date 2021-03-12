import { Component, OnInit } from '@angular/core';
import { MinRequiredTimeDto } from 'src/app/proxy/dtos';
import { MinRequiredTimeService } from 'src/app/proxy/controllers';

@Component({
  selector: 'app-min-required-time',
  templateUrl: './min-required-time.component.html',
  styleUrls: ['./min-required-time.component.scss'],
})
export class MinRequiredTimeComponent implements OnInit {

  private mrtDtoArr: Array<MinRequiredTimeDto> = [];

  constructor(private mrtService: MinRequiredTimeService) 
  { 
    this.loadAllMrt();
  }

  ngOnInit() {}

  private loadAllMrt()
  {
    this.mrtService.getList()
      .subscribe(data => {
        this.mrtDtoArr = data;
      }, err => {});
  }

  private ondelete()
  {
    this.loadAllMrt();
  }
}
