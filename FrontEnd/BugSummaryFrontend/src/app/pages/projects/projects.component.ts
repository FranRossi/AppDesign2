import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsTableComponent } from './projects-table/projects-table.component';


@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
  
})
export class ProjectsComponent {
  success :string 
  error :string 
  @ViewChild(ProjectsTableComponent) projectsTable: ProjectsTableComponent;
  constructor(){ }

}
