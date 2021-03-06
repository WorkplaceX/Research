import { Component } from '@angular/core';
import { DataService, Data } from './dataService';

@Component({
  selector: 'my-app',
  template: '<h1>My First Angular App ({{ data.Name }})</h1><button (click)="click($event)">Hello</button>',
  providers: [DataService]  
})
export class AppComponent { 
  data: Data;

  constructor(dataService: DataService){
    this.data = dataService.data;
  }

  click(event){
    this.data.Name += ".";
  }
}

