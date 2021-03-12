import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { StoreChainService } from 'src/app/proxy/controllers';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { Base64Validator } from 'src/app/validators/base64.validator';
import { ActivatedRoute } from '@angular/router';
import { StoreChainDto, StoreChainCreateDto, StoreChainUpdateDto, ThumbnailDto } from 'src/app/proxy/dtos';
import { NavController } from '@ionic/angular';
import { GalleryReaderComponent } from 'src/app/shared/components/gallery-reader/gallery-reader.component';
import { StoreChainModule } from '../store-chain.module';
import { Base64ImageValidator } from 'src/app/validators/base64Image.validator';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;

  private isAwaiting: boolean = false;  

  private isUpdatingForm: boolean = false;

  private storeChainDto: StoreChainDto;

  @ViewChild(GalleryReaderComponent)
  private galleryReader: GalleryReaderComponent;

  constructor(
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private storeChainService: StoreChainService) 
  { }

  ngOnInit() 
  { 
    this.configure();
    this.configureFormGroup();
  }

  private configure()
  {
    this.activatedRoute.params
      .subscribe(params => {
        var id = params['storeChainId'];
        if (id && !isNaN(id)) {
          this.isUpdatingForm = true;
          this.loadStoreChain(id);
        }
      });
  }

  private loadStoreChain(id: number)
  {
    this.storeChainService.getById(id)
      .subscribe(dto => {
        this.storeChainDto = dto;
        this.setupUpdatingForm(dto);
      }, err => {
        console.error('Unable to retrieve the storeChain');
        this.navController.back();
      });
  }

  private setupUpdatingForm(dto: StoreChainDto)
  {
    this.formGroup.get('storeChainName').setValue(dto.storeChainName);
    this.formGroup.get('storeChainDescription').setValue(dto.storeChainDescription);

    this.storeChainService.getLogoById(dto.id)
      .subscribe(thumbnailDto => {
        this.galleryReader.setDataUri(thumbnailDto.base64DataUrl, false);
        this.formGroup.get('storeChainLogo').setValue(thumbnailDto.base64DataUrl);
        this.formGroup.get('storeChainLogo').markAsPristine();
      }, err => {});
  }

  private onLogoLoad(ev: CustomEvent<string>)
  {
    var dataUri = ev.detail;
    this.formGroup.get('storeChainLogo').setValue(dataUri);
    this.formGroup.get('storeChainLogo').markAsDirty();
  }

  private configureFormGroup()
  {
    this.formGroup = new FormGroup({
      storeChainName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      storeChainDescription: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.descriptionMinLength),
        Validators.maxLength(ModelConstraints.descriptionMaxLength),
        Validators.pattern(ModelConstraints.descriptionAllowedCharacterPattern)
      ])),
      storeChainLogo: new FormControl('', Validators.compose([
        Validators.required                   
      ]), Validators.composeAsync([
        Base64ImageValidator.asyncValidator        
      ])),
    });
  }

  private save()
  {
    if (this.formGroup.invalid) return;

    if (this.isUpdatingForm) {
      this.update();
    }
    else {
      this.create();
    }
  }

  private create()
  {
    var dto = this.getCreateDto();
    this.storeChainService.createByDto(dto)
      .subscribe(data => {
        this.isUpdatingForm = true;
        this.loadStoreChain(data.id);
      }, err => {

      });
  }

  private update()
  {
    var dto = this.getUpdateDto();
    this.storeChainService.updateByIdAndDto(this.storeChainDto.id, dto)
      .subscribe(data => {
        if (this.formGroup.get('storeChainLogo').dirty) {
          this.updateLogo();
        }
      }, err => { });
  }

  private updateLogo()
  {
    var b64 = this.formGroup.get('storeChainLogo').value as string;
    var dto = {} as ThumbnailDto;
    dto.base64DataUrl = b64;    
    this.storeChainService.updateLogoByIdAndDto(this.storeChainDto.id, dto)
      .subscribe(data => {
      }, err => {});
  }

  private getCreateDto()
  {
    var dto = {} as StoreChainCreateDto;

    dto.storeChainName = this.formGroup.get('storeChainName').value;
    dto.storeChainDescription = this.formGroup.get('storeChainDescription').value;
    dto.logoBase64 = this.formGroup.get('storeChainLogo').value;

    return dto;
  }

  private getUpdateDto()
  {
    var dto = {} as StoreChainUpdateDto;

    dto.storeChainName = this.formGroup.get('storeChainName').value;
    dto.storeChainDescription = this.formGroup.get('storeChainDescription').value;

    return dto;
  }
}
