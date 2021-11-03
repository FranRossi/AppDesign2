import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ProjectAddComponent} from '../../pages/projects/project/addProject/addProject.component';
import {ProjectEditComponent} from '../../pages/projects/project/editProject/editProject.component';
// import { ToastrModule } from 'ngx-toastr';

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
    UserProfileComponent,
    ProjectsComponent,
    ProjectAddComponent,
    ProjectEditComponent
  ]
})

export class AdminLayoutModule {}
