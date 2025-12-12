import { Component } from '@angular/core';

@Component({
    selector: 'app-home',
    imports: [],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent {
  summaryChatView: any = [500, 500];
  summaryDataSource: any = [];

  constructor() { }

}
