import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import {TesterLayoutRoutes} from './tester-layout.routing';
import {HttpClientModule} from '@angular/common/http';
import {ClipboardModule} from 'ngx-clipboard';
import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugEditComponent} from '../../pages/bugs/bug-editor/bug-edit.component';
import { ComponentsModule } from 'src/app/components/components.module';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(TesterLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ComponentsModule
  ],
  declarations: [
    BugsComponent,
    BugEditComponent
  ]
})
export class TesterLayoutModule { }
