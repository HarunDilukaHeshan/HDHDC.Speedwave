import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/proxy/controllers';
import { CategoryDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
})
export class CategoriesComponent implements OnInit {

  private categoryDtoArr: CategoryDto[] = [];

  constructor(
    private categoryService: CategoryService) 
    {
      this.loadAllCategory();             
    }

  ngOnInit() {}

  onItemDelete(ev: CustomEvent)
  {
    this.loadAllCategory();
  }

  private loadAllCategory()
  {
    this.categoryService.getAll()
      .subscribe(result => {
        this.categoryDtoArr = result;        
      }, err => {});
  }
}
