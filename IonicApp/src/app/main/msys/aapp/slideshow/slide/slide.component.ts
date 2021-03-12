import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { MediaConstraints } from 'src/app/constraints/media-constriants';
import { ModelConstraints } from 'src/app/constraints/model-constraints';
import { Controllers } from 'src/app/proxy';
import { SlideShowService } from 'src/app/proxy/controllers';
import { SlideShowPictureDto } from 'src/app/proxy/dtos';
import { GalleryReaderController } from 'src/app/shared/dependencies/gallery-reader-controller.injectable';
import { Base64ImageValidator } from 'src/app/validators/base64Image.validator';

@Component({
  selector: 'app-slide',
  templateUrl: './slide.component.html',
  styleUrls: ['./slide.component.scss'],
})
export class SlideComponent implements OnInit {

  private hasLink: boolean = false;
  private hasItemId: boolean = false;
  private imgB64: string = '';

  private formGroup: FormGroup;
  private dto: SlideShowPictureDto;
  private isWaiting: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private navCtrl: NavController,
    private slideShowService: SlideShowService,
    private galleryReaderCtrl: GalleryReaderController) { 
    this.configureFormGroup();
  }

  ngOnInit() { 
    this.init();
  }

  private init() {
    this.formGroup.get('hasLink').valueChanges.subscribe(value => { this.hasLink = value; });
    this.formGroup.get('itemId').valueChanges.subscribe(value => { this.hasItemId = value && !isNaN(value); });

    this.activatedRoute.params
      .subscribe(params => {
        var fileName = params['fileName'];
        if (fileName) {
          this.loadSlideShowPicDto(fileName);
        }
      });
  }

  private loadSlideShowPicDto(fileName: string) {
    this.slideShowService.getByFileName(fileName)
      .subscribe(dto => {
        this.dto = dto;
        this.setFormGroupValues(dto);
      }, err => {
        this.navCtrl.back();
      });
  }  

  private async openGallery() {
    var picker = await this.galleryReaderCtrl.create({
      width: MediaConstraints.slideShowWidth,
      height: MediaConstraints.slideShowHeight      
    });

    var imgB64 = await picker.present();

    if (imgB64) {
      this.formGroup.get('slideImgB64').setValue(imgB64.dataUri);
      this.imgB64 = imgB64.dataUri;
    }
  }

  private configureFormGroup() {
    this.formGroup = new FormGroup({
      slideImgB64: new FormControl('', Validators.compose([
        Validators.required
      ])        
      , Validators.composeAsync([
        Base64ImageValidator.asyncValidator
      ])),
      name: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(ModelConstraints.nameMinLength),
        Validators.pattern(ModelConstraints.nameAllowedCharacterPattern)
      ])),
      hasLink: new FormControl(false),
      itemId: new FormControl('', Validators.min(1)),
      keywords: new FormControl('', Validators.compose([
        Validators.minLength(1)
      ]))
    });
  }

  private setFormGroupValues(dto: SlideShowPictureDto) {
    if (!dto) throw Error('Invalid arguments');

    var b64Img = dto.base64Picture;
    this.formGroup.get('name').setValue(dto.name);
    this.formGroup.get('slideImgB64').setValue(b64Img);
    this.formGroup.get('hasLink').setValue(dto.uri && dto.uri != '');

    this.imgB64 = b64Img;
    
    if (dto.uri == null || dto.uri == '') return;

    var id = this.getItemId(dto.uri);
    if (id > -1) this.formGroup.get('itemId').setValue(id);
    this.formGroup.get('keywords').setValue(this.getKeywords(dto.uri));
  }

  private saveChanges() {
    if (this.formGroup.invalid) return;

    this.isWaiting = true;
    if (this.dto) {
      this.update();
    }
    else {
      this.Create();
    }
  }

  private Create() {
    if (this.formGroup.invalid) return;
    var dto = this.getDto();

    this.slideShowService.createByDto(dto)
      .subscribe(dto => {
        console.log(dto);
      }, err => {

      }, () => {
        this.isWaiting = false;
      });
  }

  private update() {
    if (this.formGroup.invalid) return;
    var dto = this.getDto();

    this.slideShowService.updateByFileNameAndDto(this.dto.fileName, dto)
      .subscribe(dto => {
        console.log(dto);
      }, err => {
        this.navCtrl.back();
      }, () => {
        this.isWaiting = false;
      });
  }

  private getDto() {
    var dto = {} as SlideShowPictureDto;
    dto.name = this.formGroup.get('name').value;
    dto.base64Picture = this.formGroup.get('slideImgB64').value;
    
    var url: string = 'item';
    url += (this.hasItemId)? '/' + this.formGroup.get('itemId').value : ('?keywords=' + this.formGroup.get('keywords').value);

    dto.uri = (this.hasLink)? url : '';

    console.log(dto);
    return dto;
  }

  private getItemId(link: string) {
    var pattern = /\/\d+/;
    var match = link.match(pattern);
    var id: number = -1;
    if (match && match.length > 0) {
      id = parseInt(match[0].substr(1));
    }

    return id;
  }

  private getKeywords(link: string) {
    var pattern = /keywords=[\w ]*/i;
    var match = link.match(pattern);
    var val = '';
    if (match && match.length > 0) {
      val = match[0].substr(match[0].indexOf('=') + 1);
    }

    console.log(val);
    return val;
  }
}
