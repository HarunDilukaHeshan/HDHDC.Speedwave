import { Component, Input, OnInit } from '@angular/core';
import { StoreBranchService } from 'src/app/proxy/controllers';
import { StoreBranchDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-avai-stores',
  templateUrl: './avai-stores.component.html',
  styleUrls: ['./avai-stores.component.scss'],
})
export class AvaiStoresComponent implements OnInit {

  @Input() private cityId: number = 0;

  private dtoArr: Array<StoreBranchDto> = [];

  constructor(
    private storeBranchService: StoreBranchService) 
  { }

  ngOnInit() {
    this.loadAllStoreBranches(this.cityId);
  }

  private loadAllStoreBranches(cityId: number) {
    this.storeBranchService.getAllByCityId(cityId)
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
      }, err => {});
  }

}
