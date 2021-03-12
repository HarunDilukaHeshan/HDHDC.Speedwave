import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
  animations: [
    trigger('welcomeHeader', [      
      state('welcome', style({
        opacity: 1
      })),
      transition(':enter', [
        style({ opacity: 0 }),
        animate('3s')
      ])
    ])
  ]
})
export class WelcomeComponent implements OnInit {

  constructor(
    private navCtrl: NavController) 
  { }

  ngOnInit() { }

  private skip() {
    this.navCtrl.navigateForward('msys/capp');
  }
}
