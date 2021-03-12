import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { DistanceChargeService } from 'src/app/proxy/controllers';
import { DistanceChargeCreateUpdateDto, DistanceChargeDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private isUpdatingForm: boolean = false;

  private formGroup: FormGroup;

  private distanceChargeDto: DistanceChargeDto;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private distanceChargeService: DistanceChargeService) 
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.init();    
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var distanceChargeId = params['distanceChargeId'];
        if (distanceChargeId && !isNaN(distanceChargeId)) {
          this.isUpdatingForm = true;
          this.loadDistanceChargeDto(distanceChargeId);
        }
      });
  }

  private loadDistanceChargeDto(id: number) {
    this.distanceChargeService.getById(id)
      .subscribe(dto => {
        this.distanceChargeDto = dto;
      }, err => { });
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      charge: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(0.01)
      ])),
      from: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(0)
      ]))
    });
  }

  private save() {
    if (this.formGroup.invalid || this.isWaiting) return;

    if (this.isUpdatingForm)
      this.update();
    else
      this.create();
  }

  private create() {
    var dto = this.getDto();
    this.isWaiting = true;

    this.distanceChargeService.createByDto(dto)
      .subscribe(data => {
        this.isWaiting = false;
        this.isUpdatingForm = true;
        this.loadDistanceChargeDto(data.id);
      }, err => { 
        this.isWaiting = false;
      });
  }

  private update() {
    var dto = this.getDto();
    this.isWaiting = true;

    this.distanceChargeService.updateByIdAndDto(this.distanceChargeDto.id, dto)
      .subscribe(data => {
        this.isWaiting = false;
      }, err => {
        this.isWaiting = false;
      });
  }

  private getDto() {
    var dto = {} as DistanceChargeCreateUpdateDto;

    dto.charge = this.formGroup.get('charge').value as number;
    dto.from = this.formGroup.get('from').value as number;

    return dto;
  }
}
