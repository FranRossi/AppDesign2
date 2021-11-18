import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BugsTableComponent } from '../pages/bugs/bugs-table/bugs-table.component';
import { AssignmentsTableComponent } from '../pages/assignments/assignments-table/assignments-table.component';
import { FormsModule } from '@angular/forms';
import { ProjectsComponent } from '../pages/projects/projects.component';
import { ProjectsTableComponent } from '../pages/projects/projects-table/projects-table.component';
import { ProjectVisualizerComponent } from '../pages/projects/project-visualizer/project-visualizer.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    FormsModule
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    BugsTableComponent,
    AssignmentsTableComponent,
    ProjectsComponent,
    ProjectsTableComponent,
    ProjectVisualizerComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    BugsTableComponent,
    AssignmentsTableComponent,
    ProjectsComponent,
    ProjectsTableComponent,
    ProjectVisualizerComponent
  ]
})
export class ComponentsModule { }
