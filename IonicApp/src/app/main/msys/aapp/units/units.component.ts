import { Component, OnInit } from '@angular/core';
import { UnitService } from 'src/app/proxy/controllers';
import { UnitDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.scss'],
})
export class UnitsComponent implements OnInit {

  private unitDtos: UnitDto[] = [];

  constructor(
    private unitService: UnitService) 
    { 
      this.loadUnitDtos();
    }

  ngOnInit() {}

  private loadUnitDtos()
  {
    this.unitService.getAll()
        .subscribe(data=>{
          this.unitDtos = data;
        });
  }

  private ondelete()
  {
    this.loadUnitDtos();    
  }
}
