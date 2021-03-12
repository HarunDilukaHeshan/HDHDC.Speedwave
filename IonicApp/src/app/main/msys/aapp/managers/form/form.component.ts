import { ValidationErrorComponent } from '@abp/ng.theme.basic';
import { Component, OnInit, ViewChild } from '@angular/core';
import { async } from '@angular/core/testing';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IonSlides, NavController } from '@ionic/angular';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { DistrictService, ManagerService, UserService } from 'src/app/proxy/controllers';
import { DistrictDto, ManagerCreateDto, UserCreateDto } from 'src/app/proxy/dtos';
import { UsernameAvailabilityValidator } from 'src/app/validators/usernameAvailability.validator';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private isWaiting: boolean = false;

  private formGroup: FormGroup;  

  private districtDtoArr: Array<DistrictDto> = [];

  @ViewChild('slideComp')
  private slidesPages: IonSlides;

  constructor(
    private userService: UserService,
    private navController: NavController,
    private districtService: DistrictService,
    private managerService: ManagerService) {
  }

  ngOnInit() {    
    this.configureFormGroup();    
    this.loadAllDistrict(); 
  }

  private slidesCtrl = {
    onSlideChange: async () => {
      this.slidesCtrl.isBeginning = await this.slidesPages.isBeginning();
      this.slidesCtrl.isEnd = await this.slidesPages.isEnd();
    },
    isBeginning: true,
    isEnd: false
  };

  private configureFormGroup() {
    this.formGroup = new FormGroup({      
      name: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      surname: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.email
      ])),
      phoneNo: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(ModelConstraints.phoneNoPattern)
      ])),
      districtId: new FormControl('', Validators.compose([
        Validators.required
      ])),
      username: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z][a-zA-Z0-9]/)        
      ]), UsernameAvailabilityValidator.getAsyncValidator(this.userService)),
      password: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/[a-zA-Z0-9]/)
      ]))
    });
  }

  private loadAllDistrict() {
    this.districtService.getAll()
      .subscribe(dtoArr => {
        this.districtDtoArr = dtoArr;
      }, err => {
      });
  }

  private save() {
    if (this.isWaiting) return;
    this.isWaiting = true;

    var dto = this.getDto();

    this.managerService.createByDto(dto)
      .subscribe(data => {
        this.isWaiting = false;
        this.navController.back();
      }, err => {
        this.isWaiting = false;        
      });
  }

  private getDto() {
    var dto = {} as ManagerCreateDto;
    var cDto = {} as UserCreateDto;

    dto.userCreateDto = cDto;

    dto.districtID = this.formGroup.get('districtId').value as string;
    cDto.name = this.formGroup.get('name').value as string;
    cDto.surname = this.formGroup.get('surname').value as string;
    cDto.phoneNumber = this.formGroup.get('phoneNo').value as string;
    cDto.emailAddress = this.formGroup.get('email').value as string;
    cDto.userName = this.formGroup.get('username').value as string;
    cDto.password = this.formGroup.get('password').value as string;

    return dto;
  }

  
}
