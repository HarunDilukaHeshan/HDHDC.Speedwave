import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { SubtotalPercentageService } from 'src/app/proxy/controllers';
import { SubtotalPercentageCreateUpdateDto, SubtotalPercentageDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private isUpdatingForm: boolean = false;

  private formGroup: FormGroup;
  
  private subtotalPercentageDto: SubtotalPercentageDto;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private subtotalPercentageService: SubtotalPercentageService) 
  { }

  ngOnInit() {
    this.configureFormGroup();
    this.init();
  }

  private init() {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['subtotalPercentageId'];
        if (id && !isNaN(id)) {
          this.isUpdatingForm = true;
          this.loadSubtotalPerDto(id);
        }
      });
  }

  private loadSubtotalPerDto(id: number) {
    this.subtotalPercentageService.getById(id)
      .subscribe(dto => {
        this.subtotalPercentageDto = dto
        this.setupFormGroup(dto);
      }, err => { 
        this.navController.back();
      });
  }

  private setupFormGroup(dto: SubtotalPercentageDto) {
    this.formGroup.get('from').setValue(dto.from);
    this.formGroup.get('percentage').setValue(dto.percentage);
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      from: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(0)
      ])),
      percentage: new FormControl('', Validators.compose([
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

    this.subtotalPercentageService.createByDto(dto)
      .subscribe(data => {
        this.isWaiting = false;
        this.isUpdatingForm = true;
        this.subtotalPercentageDto = data;
      }, err => { 
        this.isWaiting = false;
      });
  }

  private update() {
    var dto = this.getDto();
    this.isWaiting = true;

    this.subtotalPercentageService.updateByIdAndDto(this.subtotalPercentageDto.id, dto)
      .subscribe(data => {
        this.isWaiting = false;
        this.subtotalPercentageDto = data;
      }, err => { 
        this.isWaiting = false;
      });
  }

  private getDto() {
    var dto = {} as SubtotalPercentageCreateUpdateDto;

    dto.from = this.formGroup.get('from').value;
    dto.percentage = this.formGroup.get('percentage').value

    return dto;
  }
}
