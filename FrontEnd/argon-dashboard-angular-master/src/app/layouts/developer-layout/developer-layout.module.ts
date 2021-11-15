import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import {HttpClientModule} from '@angular/common/http';
import {ClipboardModule} from 'ngx-clipboard';
import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugEditComponent} from '../../pages/bugs/bug-editor/bug-edit.component';
import {DeveloperLayoutRoutes} from './developer-layout.routing';


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
  ]
})
export class DeveloperLayoutModule { }
