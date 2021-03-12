import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { Base64Validator } from 'src/app/validators/base64.validator';
import { ActivatedRoute, UrlMatchResult } from '@angular/router';
import { GalleryReaderComponent } from 'src/app/shared/components/gallery-reader/gallery-reader.component';
import { NavController } from '@ionic/angular';
import { CategoryService } from 'src/app/proxy/controllers';
import { CategoryDto, CategoryCreateDto, CategoryUpdateDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-cat-form',
  templateUrl: './cat-form.component.html',
  styleUrls: ['./cat-form.component.scss'],
})
export class CatFormComponent implements OnInit {

  private categoryFormGroup: FormGroup = null;
  private isUpdateForm: boolean = false;
  private categoryId: number = -1;

  @ViewChild(GalleryReaderComponent)
  private galleryReader: GalleryReaderComponent;

  constructor(
    private activatedRoute: ActivatedRoute, 
    private navController: NavController,
    private categoryService: CategoryService) 
  { 
    this.configureFromGroup();
    this.configureUpdatingForm();    
  }

  ngOnInit() {}

  loadImg(ev: CustomEvent)
  {
    var dataUri = ev.detail as string;
    var b64Part = dataUri.substring(dataUri.indexOf(',')+1);
    this.categoryFormGroup.get('categoryThumbnailBase64').setValue(b64Part);
  }

  private configureUpdatingForm() {
    this.activatedRoute.params.subscribe(params => {
      var id = params['categoryId'];
      if (id && !isNaN(id)) {
        this.isUpdateForm = true;
        this.loadCurrentCategory(id);
      }
    });
  }

  private loadCurrentCategory(id: number) {
    this.categoryService.getById(id)
      .subscribe(categoryDto => {
        this.categoryId = id;
        this.categoryFormGroup.get('categoryName').setValue(categoryDto.categoryName);
        this.categoryFormGroup.get('categoryDescription').setValue(categoryDto.categoryDescription);
        this.categoryService.getThumbnailByThumbnail(categoryDto.categoryThumbnail)
          .subscribe(data => {
            if (this.galleryReader)
              this.galleryReader.setDataUri(data.base64DataUrl)
          });
      });
  }

  private configureFromGroup() {
    this.categoryFormGroup = new FormGroup({
      categoryName: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.maxLength(ModelConstraints.nameMaxLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern),
      ])),
      categoryDescription: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.descriptionMinLength),
        Validators.maxLength(ModelConstraints.descriptionMaxLength),
        Validators.pattern(ModelConstraints.descriptionAllowedCharacterPattern)
      ])),
      categoryThumbnailBase64: new FormControl('', Validators.compose([
        Validators.required,
        Base64Validator.validator
      ]))
    });
  }

  private save()
  {
    if (this.isUpdateForm)
        this.updateCategory(this.categoryId);
    else
        this.insertCategory();
  }

  private insertCategory()
  {
    var createDto = this.getCreateDto();
    this.categoryService.createByDto(createDto)
        .subscribe(categoryDto => {
          this.navController.back();
        });
  }

  private updateCategory(categoryId: number)
  {    
    var updateDto = this.getUpdateDto(categoryId);
    this.categoryService.updateByIdAndDto(categoryId, updateDto)
        .subscribe(categoryDto => {
          this.navController.back();
        });
  }

  private getCreateDto()
  {
    var dto = {} as CategoryCreateDto;
    dto.categoryName = this.categoryFormGroup.get('categoryName').value as string;
    dto.categoryDescription = this.categoryFormGroup.get('categoryDescription').value as string;
    dto.thumbnailBase64 = this.categoryFormGroup.get('categoryThumbnailBase64').value as string;
    return dto;
  }

  private getUpdateDto(categoryId: number)
  {
    var dto = {} as CategoryUpdateDto;
    dto.id = categoryId;
    dto.categoryName = this.categoryFormGroup.get('categoryName').value as string;
    dto.categoryDescription = this.categoryFormGroup.get('categoryDescription').value as string;
    dto.thumbnailBase64 = this.categoryFormGroup.get('categoryThumbnailBase64').value as string;
    return dto;
  }
}
