import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { ItemService, QuantityService, MinRequiredTimeService, CategoryService } from 'src/app/proxy/controllers';
import { ItemDto, ItemCreateDto, QuantityDto, MinRequiredTimeDto, CategoryDto, ThumbnailDto, ItemCategoryDto, ItemUpdateDto, BlobFileDto } from 'src/app/proxy/dtos';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { CustomDetail, ImgDataUri, ProductPicsComponent } from './product-pics/product-pics.component';
import { GalleryReaderController } from 'src/app/shared/dependencies/gallery-reader-controller.injectable';
import { concat, Observable } from 'rxjs';
import { Base64ImageValidator } from 'src/app/validators/base64Image.validator';
import { async } from '@angular/core/testing';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {

  private formGroup: FormGroup;
  private isWaiting: boolean = false;
  private isUpdatingForm: boolean = false;
  private itemDto: ItemDto;  
  private itemCatDtoArr: Array<ItemCategoryDto> = [];
  private quantityDtoArr: Array<QuantityDto> = [];
  private mrtDtoArr: Array<MinRequiredTimeDto> = [];  
  private categoryDtoArr: Array<CategoryDto> = [];
  private picsCustomDetail: CustomDetail;  

  @ViewChild('productPics')
  private productPics: ProductPicsComponent;

  constructor(
    private galleryReaderCtrl: GalleryReaderController,
    private itemService: ItemService,
    private quantityService: QuantityService,
    private minRequiredTimeService: MinRequiredTimeService,
    private navController: NavController,
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService) {    
  }

  ngOnInit() {    
    this.init();
    this.configureFormGroup();
  }

  private async init() {
    this.activatedRoute.params
      .subscribe(async params => {

        await this.loadAllMrtDto();
        await this.loadAllQuantityDto();
        await this.loadAllCategoryDto();

        var id = params['itemId'];

        if (id && !isNaN(id)) {
          this.isUpdatingForm = true;
          var dto = await this.loadItemDto(id);          
          await this.setupFormValues(dto);
          await this.resetProductPics(id, dto.itemThumbnail);
        }        
      });    
  }

  private async loadItemDto(id: number) {
    return new Promise<ItemDto>((resolve, reject) => {
      this.itemService.getById(id)
        .subscribe(dto => {
          this.itemDto = dto;
          resolve(dto);
        }, err => {
          this.navController.back();
          reject();
        }, () => {
          reject();
        });
    });
  }

  private loadAllMrtDto() {
    return new Promise<MinRequiredTimeDto[]>((resolve, reject) => {
      this.minRequiredTimeService.getList()
        .subscribe(dtoArr => {
          this.mrtDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => { reject(err); }, () => { reject(); });
    });
  }

  private loadAllQuantityDto() {
    return new Promise<QuantityDto[]>((resolve, reject) => {
      this.quantityService.getAll()
        .subscribe(dtoArr => {
          this.quantityDtoArr = dtoArr;
          resolve(dtoArr);
        }, err => { reject(err); }, () => { reject(); });
    });
  }

  private loadAllCategoryDto() {
    return new Promise<CategoryDto[]>((resolve, reject) => {
      this.categoryService.getAll()
        .subscribe(dtoArr => {
          this.categoryDtoArr = dtoArr;
          resolve(dtoArr); 
        }, err => { reject(err); }, () => { reject(); });
    });
  }
  
  private configureFormGroup() {
    this.formGroup = new FormGroup({
      itemName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      itemDescription: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.descriptionMinLength),
        Validators.maxLength(ModelConstraints.descriptionMaxLength),
        Validators.pattern(ModelConstraints.descriptionAllowedCharacterPattern)
      ])),
      itemPrice: new FormControl('', Validators.compose([
        Validators.required,
        Validators.min(ModelConstraints.minItemPrice)
      ])),
      quantity: new FormControl('', Validators.compose([
        Validators.required
      ])),
      minRequiredTime: new FormControl('', Validators.compose([
        Validators.required
      ])),
      categories: new FormControl([], Validators.compose([
        Validators.required,
        Validators.minLength(1)
      ])),
      thumbnailImgDataUri: new FormControl('', Validators.compose([
        Validators.required,
        Base64ImageValidator.validator
      ]))
    });
  }

  private async setupFormValues(dto: ItemDto) {
    this.formGroup.get('itemName').setValue(dto.itemName);
    this.formGroup.get('itemDescription').setValue(dto.itemDescription);
    this.formGroup.get('itemPrice').setValue(dto.itemPrice);
    this.formGroup.get('quantity').setValue(dto.quantityId);
    this.formGroup.get('minRequiredTime').setValue(dto.minRequiredTimeId);    

    this.formGroup.get('categories').setValue(await this.loadAllItemCategoryDto(dto.id));

    // this.formGroup.get('thumbnailImgDataUri')
  }

  private loadAllItemCategoryDto(id: number) {
    return new Promise<number[]>((resolve, reject) => {
      this.itemService.getAllItemCategoryById(id)
        .subscribe(dtoArr => {
          this.itemCatDtoArr = dtoArr;
          var itemCategoryDtoArr = dtoArr.map(val => val.categoryID);          
          resolve(itemCategoryDtoArr);
        }, err => { reject(err); }, () => { reject(); });
    });
  }

  private async openFilePicker() {
    var reader = await this.galleryReaderCtrl.create({});
    var f = await reader.present();
    this.productPics.addImg(new ImgDataUri(f.dataUri));
  }

  private async resetProductPics(id: number, thumbName: string) {    
    var itemPics = await this.loadAllItemPics(id);    
    var pics: Array<ImgDataUri> = [];    
    itemPics.forEach(dto => { pics.push(new ImgDataUri(dto.base64DataUri, dto.fileName)) });

    var i = pics.findIndex(val => val.fileName === thumbName);

    this.formGroup.get('thumbnailImgDataUri').setValue(pics[i].dataUri);
    this.productPics.reset(pics, pics[i]);
  }

  private loadAllItemPics(id: number): Promise<BlobFileDto[]> {
    return new Promise((resolve, reject) => {
      this.itemService.getAllItemPicById(id)
        .subscribe(dtoArr => { resolve(dtoArr); }, err => { reject(err) }, () => { reject(); });
    });
  }

  private onReset(cd: CustomDetail) {
    this.picsCustomDetail = cd;
  }

  private onImgAdd(cd: CustomDetail) {
    this.picsCustomDetail = cd;
  }

  private onImgRemove(cd: CustomDetail) {
    this.picsCustomDetail = cd;
  }

  private onThumbChange(cd: CustomDetail) {
    this.picsCustomDetail = cd;
    this.formGroup.get('thumbnailImgDataUri').setValue(cd.thumbnailImgDataUri.dataUri);
  }

  private saveAll() {
   // this.isWaiting = true;
    this.save().then(() => {
    //  this.isWaiting = false;
    }, () => {
     // this.isWaiting = false;
    });
  }

  private async save() {
    if (this.isWaiting && this.formGroup.invalid) return;
    if (this.isUpdatingForm) {     
      this.isWaiting = true; 
      await this.updateItemDto(this.itemDto.id);
      await this.updateItemCatDto(this.itemDto.id);
      await this.updateThumbnail(this.itemDto.id);
      await this.addRemovePics(this.itemDto.id);
      this.isWaiting = false;
      this.init();
    }
    else {
      this.isWaiting = true;
      var dto = await this.createItemDto();
      await this.updateItemCatDto(dto.id);
      await this.addRemovePics(dto.id);
      this.isWaiting = false;
    }
  }

  private createItemDto() {
    return new Promise<ItemDto>((resolve, reject) => {
      var dto = this.getItemCreateDto();
      this.itemService.createByDto(dto)
        .subscribe(dto => { resolve(dto); }, err => { reject(err) }, () => { reject(); });
    });
  }

  private async updateItemDto(id: number) {
    return new Promise<ItemDto>((resolve, reject) => {
      var dto = this.getItemUpdateDto();
      this.itemService.updateByIdAndDto(id, dto)
        .subscribe(itemDto => { resolve(itemDto); }, err => { reject(err); }, () => { reject(); });
    });
  }

  private async updateItemCatDto(itemId: number) {
    var dtoArr = this.getItemCatDtoArr(itemId);

    return new Promise((resolve, reject) => {
      this.itemService.updateItemCategoryByIdAndDtosArr(itemId, dtoArr)
        .subscribe(() => { resolve() }, err => { reject(err); }, () => { reject() });
    });
  }

  private async updateThumbnail(id: number) {
    var thumb = this.picsCustomDetail.thumbnailImgDataUri;
    if (thumb == null || thumb.fileName !== '' || thumb.fileName === this.itemDto.itemThumbnail) return;    
    var dto = await this.addItemPic(id, thumb.dataUri);
    var itemDto = this.getItemUpdateDto();
    itemDto.itemThumbnail = dto.fileName;

    return new Promise<ItemDto>((resolve, reject) => {      
      this.itemService.updateByIdAndDto(id, itemDto)
        .subscribe(itemDto => { resolve(itemDto); }, err => { reject(err); }, () => { reject(); });
    });
  }

  private async addRemovePics(id: number) {
    var addedPics = this.picsCustomDetail.addedImgDataUriArr;
    var removedPics = this.picsCustomDetail.removedImgDataUriArr;
    var thumbnailPic = this.picsCustomDetail.thumbnailImgDataUri;

    if (thumbnailPic) {
      var i = addedPics.findIndex(val => val.dataUri === thumbnailPic.dataUri);
      addedPics.splice(i, 1);
    }

    for (var i = 0; i < addedPics.length; i++)
      await this.addItemPic(id, addedPics[i].dataUri);

    for (var i = 0; i < removedPics.length; i++)
      await this.removeItemPic(id, removedPics[i].fileName);    
  }

  private addItemPic(id: number, b64Img: string): Promise<BlobFileDto> {
    return new Promise((resolve, reject) => {
      this.itemService.addItemPicByIdAndDto(id, { base64DataUri: b64Img, fileName: '' })
        .subscribe(dto => { resolve(dto); }, err => { reject(err); }, () => { reject(); });
    });
  }

  private removeItemPic(id: number, fileName: string) {
    return new Promise((resolve, reject) => {
      this.itemService.deleteItemPicByIdAndFilename(id, fileName)
        .subscribe(() => { resolve(); }, err => { reject(err); }, () => { reject(); });
    });
  }

  private getItemCreateDto() {
    var dto = {} as ItemCreateDto;

    dto.itemName = this.formGroup.get('itemName').value;
    dto.itemDescription = this.formGroup.get('itemDescription').value;
    dto.itemPrice = this.formGroup.get('itemPrice').value;
    dto.minRequiredTimeId = this.formGroup.get('minRequiredTime').value;
    dto.quantityId = this.formGroup.get('quantity').value;
    dto.thumbnailBase64 = this.picsCustomDetail.thumbnailImgDataUri.dataUri;

    return dto;
  }

  private getItemUpdateDto() {
    var dto = {} as ItemUpdateDto;    

    dto.itemName = this.formGroup.get('itemName').value;
    dto.itemDescription = this.formGroup.get('itemDescription').value;
    dto.itemPrice = this.formGroup.get('itemPrice').value;
    dto.minRequiredTimeId = this.formGroup.get('minRequiredTime').value;
    dto.quantityId = this.formGroup.get('quantity').value;

    var cd = this.picsCustomDetail, thumb = cd.thumbnailImgDataUri;    
    dto.itemThumbnail = (cd && thumb && thumb.fileName != '')? thumb.fileName : this.itemDto.itemThumbnail;

    return dto;
  }

  private getItemCatDtoArr(id: number) {
    var idArr = this.formGroup.get('categories').value as Array<number>;
    var itemCatDtoArr = [];
    idArr.forEach(catId => { itemCatDtoArr.push({ itemID: id, categoryID: catId } as ItemCategoryDto) });
    return itemCatDtoArr;
  }
}

// dto.thumbnailBase64 = this.formGroup.get('thumbnailImgDataUri').value;