import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [NgbModule.forRoot(), BrowserModule],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
  providers: [
  ]
})
export class AppModule { }
