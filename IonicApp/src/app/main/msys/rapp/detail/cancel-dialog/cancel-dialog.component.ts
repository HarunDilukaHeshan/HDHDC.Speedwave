import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { OrderService } from 'src/app/proxy/controllers';
import { CancellationReasonDto } from 'src/app/proxy/dtos';

@Component({
  selector: 'app-cancel-dialog',
  templateUrl: './cancel-dialog.component.html',
  styleUrls: ['./cancel-dialog.component.scss'],
})
export class CancelDialogComponent implements OnInit {

  private crDtoArr: Array<CancellationReasonDto> = [];

  private formGroup: FormGroup = new FormGroup({ cReasonId: new FormControl(), description: new FormControl() });

  constructor(
    private orderService: OrderService,
    private modalCtrl: ModalController) 
  { 
    this.init();
  }

  ngOnInit() 
  { }

  private async init() {    
    this.loadAllCr();
  }

  public async dismiss(role: string) {
    return await this.modalCtrl.dismiss({}, role);
  }

  public async done() {
    if (this.formGroup.invalid) return;

    var des = this.formGroup.get('description').value;
    var crId = this.formGroup.get('cReasonId').value;
    var co = { crId: crId, description: des };
    await this.modalCtrl.dismiss(co, 'done');
  }

  private getCRDescription(id: number) {
    var i = this.crDtoArr.find(e => e.id == id);
    return (i)? i.description : '';
  }

  private configureFormGroup(dtoArr: Array<CancellationReasonDto>) {
    this.formGroup = new FormGroup({
      cReasonId: new FormControl('', Validators.required),
      description: new FormControl('', Validators.compose([]) )
    }, { validators: validator(dtoArr) });
  }

  private loadAllCr() {
    this.orderService.getAllCR()
      .subscribe(dtoArr => {
        this.crDtoArr = dtoArr;
        this.configureFormGroup(dtoArr);
      }, err => { });
  }
}

const validator = (dtoArr: Array<CancellationReasonDto>): ValidatorFn => {  
  return (fg: FormGroup): ValidationErrors | null => {
    const crIdFC = fg.get('cReasonId');
    const desFC = fg.get('description');

    if (crIdFC && desFC) {      
      var des = desFC.value;      
      var el = dtoArr.find(e => e.id == crIdFC.value);
      if (!el || el.cancellationReason.toLocaleLowerCase() == 'other' && des == '') {
        desFC.setErrors({ descriptionRequired: true });
        return { descriptionRequired: true };
      }
      else if (desFC.invalid && des == '')
        desFC.setValue('');
    }

    return null;
  };
};

