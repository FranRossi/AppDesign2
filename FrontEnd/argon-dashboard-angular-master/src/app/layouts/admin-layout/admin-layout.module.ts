import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { BugReadersComponent } from '../../pages/user-profile/bug-readers.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import { RegisterComponent } from '../../pages/register/register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ProjectEditComponent} from '../../pages/projects/project/editProject.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule
  ],
  declarations: [
    DashboardComponent,
    BugReadersComponent,
    ProjectsComponent,
    ProjectEditComponent,
    RegisterComponent
  ]
})

export class AdminLayoutModule {}
