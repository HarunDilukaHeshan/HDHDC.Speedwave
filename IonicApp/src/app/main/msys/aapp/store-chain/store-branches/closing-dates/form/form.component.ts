import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { CityService, StoreBranchService } from 'src/app/proxy/controllers';
import { CityDto, StoreBranchDto, StoreClosingDateDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private formGroup: FormGroup;

  private branchId: number;

  private chainName: string = '';

  private cityName: string = '';  

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private storeBranchService: StoreBranchService) { 

   }

  ngOnInit() {
    this.configureFormGroup();
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var branchId = params['storeBranchId'];
        var chainName = params['chainName'];
        var cityName = params['cityName'];

        if (branchId && !isNaN(branchId)) {
          this.branchId = branchId;          
        } else {
          this.navController.back();
          throw Error('Must provide a branch id');
        }

        if (chainName && cityName) {
          this.chainName = chainName;
          this.cityName = cityName; }
        else {
          this.navController.back();
          throw Error('Must provide chain and city name'); }
      });
  }
  
  private configureFormGroup() {
    this.formGroup = new FormGroup({
      closingDate: new FormControl('', Validators.compose([
        Validators.required
      ]))
    });
  }

  private getMinDate() {  
    var now = new Date(Date.now());
    return (new Date(now.getFullYear(), now.getMonth(), now.getDate())).toLocalISOString();
  }

  private save() {
    if (this.formGroup.invalid || this.isWaiting) return;

    var dto = this.getCreateDto();
    this.storeBranchService.createClosingDateByIdAndDto(this.branchId, dto)
      .subscribe(data => {
        this.navController.back();
      }, err => {
        this.navController.back();
        throw Error('Unexpected error occured');
      });
  }

  private getCreateDto()
  {
    var dto = {} as StoreClosingDateDto;
    dto.storeBranchID = this.branchId;
    dto.closingDate = this.formGroup.get('closingDate').value;    
    return dto;
  }
}
