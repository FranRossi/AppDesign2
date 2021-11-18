import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { BugReadersComponent } from '../../pages/bug-readers/bug-readers.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import { RegisterComponent } from '../../pages/register/register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ProjectEditComponent} from '../../pages/projects/project/editProject.component';
import { ProjectsTableComponent } from 'src/app/pages/projects/projects-table/projects-table.component';
import { ComponentsModule } from 'src/app/components/components.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ComponentsModule
  ],
  declarations: [
    BugReadersComponent,
    ProjectsComponent,
    ProjectEditComponent,
    RegisterComponent,
    ProjectsTableComponent
  ]
})

export class AdminLayoutModule {}
