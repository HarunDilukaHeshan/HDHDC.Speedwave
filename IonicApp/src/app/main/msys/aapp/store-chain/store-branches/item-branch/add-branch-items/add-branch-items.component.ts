import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { forkJoin, from, Observable } from 'rxjs';
import { ItemService, StoreBranchService } from 'src/app/proxy/controllers';
import { ItemDto, ItemStoreBranchDto } from 'src/app/proxy/dtos';
import { SpeedySelectController, SpeedySelectData, SpeedySelectElement, SpeedySelectItem, SpeedySelectResults } from 'src/app/shared/components/speedy-select/speedy-select.controller.service';

@Component({
  selector: 'app-add-branch-items',
  templateUrl: './add-branch-items.component.html',
  styleUrls: ['./add-branch-items.component.scss'],
})
export class AddBranchItemsComponent implements OnInit {

  private itemDtoArr: Array<ItemDto> = [];
  private itemBranchDtoArr: Array<ItemStoreBranchDto> = [];
  private modal: SpeedySelectElement;

  private skipCount: number = 0;
  private maxResultCount: number = 10;
  private keywords: string = '';

  @Input('branchId')
  private branchId: number = 0;

  @Output('onDidDismiss')
  private onDidDismissEv: EventEmitter<SpeedySelectData> = new EventEmitter<SpeedySelectData>();

  @Output('onChangesSaved')
  private onSave: EventEmitter<SpeedySelectItem[]> = new EventEmitter<SpeedySelectItem[]>();

  constructor(
    private itemService: ItemService,
    private branchService: StoreBranchService,
    private ssCtrl: SpeedySelectController)
  { }

  ngOnInit() {
    this.loadAllItemBranchDto(this.branchId);
  }

  private async open() {
    this.modal = await this.ssCtrl.Create({ 
      title: 'Speedy', 
      subTitle: 'Speedy', 
      cssClass: 'bbb',
      isMultiSelect: true 
    });

    this.modal.setItems([]);
    this.modal.onSearchDidChange().subscribe(data => { this.onSearchDidChange(data.value); });
    this.modal.onScrollReachBottom().subscribe(() => { this.onScrollReachBottom(); });
    this.modal.onDidDismiss().subscribe((data) => { this.onDismiss(data); })

    this.loadAllItemBranchDto(this.branchId)
      .subscribe(() => {
        this.loadAllItemDtoWithPaging()
        .subscribe(dtoArr => { 
          this.setSpeedySelectItems(dtoArr); 
        });
      }, err => {});    

    await this.modal.present();        
  }

  private onSearchDidChange(value: string) {
    this.itemService.search(value, 0, 10)
      .subscribe(dtoArr => {
        this.skipCount = 0;
        this.itemDtoArr = dtoArr;
        this.setSpeedySelectItems(dtoArr);
      });  
  }

  private onScrollReachBottom() {
    this.loadAllItemDtoWithPaging();
  }

  private onDismiss(data: SpeedySelectData) {
    var obArr = []
    if (data.role == 'done') {
      data.data.addedItems.forEach(val => {
        obArr.push(this.addItemBranch(val.id));
      });

      forkJoin(obArr).subscribe(() => {
        this.onSave.emit(data.data.addedItems);
      }, err => { });
    }
  }
  
  private loadAllItemDtoWithPaging() {
    var ob = this.itemService.search(this.keywords, this.skipCount, this.maxResultCount);    
    ob.subscribe(dtoArr => { this.itemDtoArr = dtoArr; this.skipCount += this.maxResultCount; });
    return from(ob);
  }

  private loadAllItemBranchDto(branchId: number) {
    var ob = this.branchService.getAllItemsByIdAndKeywordsAndSkipCountAndMaxResultCountAndIncludeDetails(branchId, '');

    ob.subscribe(dtoArr => {
      this.itemBranchDtoArr = dtoArr;
    }, err => { });

    return from(ob);
  }

  private setSpeedySelectItems(dtoArr: Array<ItemDto>) {
    if (!dtoArr) throw Error('Arguments exception');
    var ssItemArr: Array<SpeedySelectItem> = [];

    dtoArr.forEach(dto => {
      var ibDto = this.itemBranchDtoArr.find(val => val.itemID == dto.id);
      ssItemArr.push({
        header: dto.itemName,
        subHeader: 'Price LKR' + dto.itemPrice,
        disabled: (ibDto !== undefined),
        selected: (ibDto !== undefined),
        id: dto.id
      });
    });

    this.modal.setItems(ssItemArr);
  }

  private addItemBranch(id: number) {
    var dto = { itemID: id } as ItemStoreBranchDto;
    var ob = this.branchService.addItemByIdAndDto(this.branchId, dto);

    return from(ob);
  }
}
