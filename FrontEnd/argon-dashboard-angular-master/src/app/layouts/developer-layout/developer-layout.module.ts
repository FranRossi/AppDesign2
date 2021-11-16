import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import {HttpClientModule} from '@angular/common/http';
import {ClipboardModule} from 'ngx-clipboard';
import {DeveloperLayoutRoutes} from './developer-layout.routing';
import {BugViewerComponent} from '../../pages/bugs/bug-viewer/bug-viewer.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(DeveloperLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule
  ],
  declarations: [
    BugViewerComponent
  ]
})
export class DeveloperLayoutModule { }
