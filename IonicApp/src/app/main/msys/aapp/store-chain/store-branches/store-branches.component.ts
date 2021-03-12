import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { StoreBranchService, StoreChainService } from 'src/app/proxy/controllers';
import { StoreBranchDto, StoreChainDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-store-branches',
  templateUrl: './store-branches.component.html',
  styleUrls: ['./store-branches.component.scss'],
})
export class StoreBranchesComponent implements OnInit {

  private storeBranchDtoArr: Array<StoreBranchDto> = [];

  private storeChainDto: StoreChainDto;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private storeChainService: StoreChainService,
    private storeBranchService: StoreBranchService) 
  { }

  ngOnInit() 
  { 
    this.init();
  }

  private init()
  {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['storeChainId'];
        if (id && !isNaN(id))
        {
          this.loadStoreChainDto(id);
          this.loadAllStoreBranches(id);
        }
        else
          this.navController.back();
      });
  }

  private loadStoreChainDto(id: number)
  {
    this.storeChainService.getById(id)
      .subscribe(dto => {
        this.storeChainDto = dto;
      }, err => {});
  }

  private loadAllStoreBranches(id: number)
  {
    // this.storeBranchService.getAll()
    //   .subscribe(data => {
    //     this.storeBranchDtoArr = data;
    //   }, err => {});
  }
}
