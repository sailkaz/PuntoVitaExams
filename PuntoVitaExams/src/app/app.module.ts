import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HttpService } from './services/httpService';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { ExaminationCommitteeHeadComponent } from './examinationCommittee/examination-committee-head.component';
import { AccountControllerComponent } from './account-controller/account-controller.component';
import { CokolwiekComponent } from './cokolwiek.component';


@NgModule({
  declarations: [
    AppComponent,
    ExaminationCommitteeHeadComponent,
    AccountControllerComponent,
    CokolwiekComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
