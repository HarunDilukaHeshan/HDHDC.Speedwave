import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { stringify } from 'querystring';
import { CategoryService } from 'src/app/proxy/controllers';
import { CategoryDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-home-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
})
export class CategoriesComponent implements OnInit {

  private dtoArr: Array<CategoryDto> = [];
  private keywords: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.loadAllCategoryDto();
  }  

  private categorySlides = {
    slidesPerView: 3,
    initialSlide: 1,
    centerInsufficientSlides: true,
    centeredSlides: true
  }; 

  private loadAllCategoryDto() {
    this.categoryService.getAll()
      .subscribe(dtoArr => {
        this.dtoArr = dtoArr;
        this.loadCategoryThumbnails(dtoArr);
      }, err => {});
  }

  private loadCategoryThumbnails(dtoArr: Array<CategoryDto>) {
    dtoArr.forEach(val => {
      this.categoryService.getThumbnailByThumbnail(val.categoryThumbnail)
        .subscribe(dto => {
          val.categoryThumbnailBase64 = dto.base64DataUrl;
        }, err => {});
    });
  }

}
