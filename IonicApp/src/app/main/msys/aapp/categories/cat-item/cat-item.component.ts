import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonAlerts, CommonAlertsButtonGroups, CommonAlertsButtons } from 'src/app/shared/dependencies/common-alerts.injectable';
import { CategoryService } from 'src/app/proxy/controllers';
import { ThumbnailDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-cat-item',
  templateUrl: './cat-item.component.html',
  styleUrls: ['./cat-item.component.scss'],
})
export class CatItemComponent implements OnInit {

  @Input("category-name")
  private categoryName: string = "";

  @Input("thumbnail")
  private thumbnailName: string = "";

  @Input("category-id")
  private categoryId: number = -1;

  @Output("ondelete")
  private onDelete: EventEmitter<CustomEvent> = new EventEmitter<CustomEvent>();

  private thumbnailDataUrl: string = "";

  constructor(
    private categoryService: CategoryService,
    private commonAlerts: CommonAlerts)
  { }

  ngOnInit() 
  {}

  ngAfterViewInit()
  {
    this.GetThumbnail(this.thumbnailName);
  }

  private async deleteItem()
  {
    var result = await this.commonAlerts
      .warningAlert('', '', 'Are you sure you want to continue?', CommonAlertsButtonGroups.YesNo);
      
    if (result == CommonAlertsButtons.No)
      return;          
      
    this.categoryService.deleteById(this.categoryId)
      .subscribe(() => {
        this.onDelete.emit(new CustomEvent('onDelete', { detail: this.categoryId}));
      });
  }

  private async GetThumbnail(thumbnailName: string): Promise<string>
  {
    if (thumbnailName == "" || thumbnailName == "")
        return "assets/item.jpg";

    this.categoryService.getThumbnailByThumbnail(thumbnailName)
        .subscribe((data: ThumbnailDto) => {
          if (data && data.base64DataUrl != "") {
              this.thumbnailDataUrl = data.base64DataUrl;
          } 
          else {
            this.thumbnailDataUrl = "assets/item.jpg";
          }
        }, err => {
          console.error(err);
        });    
  }

}
