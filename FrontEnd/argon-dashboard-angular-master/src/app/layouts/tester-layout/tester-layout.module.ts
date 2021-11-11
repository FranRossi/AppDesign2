import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import {TesterLayoutRoutes} from './tester-layout.routing';
import {HttpClientModule} from '@angular/common/http';
import {ClipboardModule} from 'ngx-clipboard';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(TesterLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule
  ],
  declarations: []
})
export class TesterLayoutModule { }
