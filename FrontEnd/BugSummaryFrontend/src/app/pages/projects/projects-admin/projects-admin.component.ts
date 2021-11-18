import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsTableComponent } from '../projects-table/projects-table.component';
import { ProjectsService } from '../projects.service';


@Component({
  selector: 'app-projects-admins',
  templateUrl: './projects-admin.component.html',
  styleUrls: ['./projects-admin.component.scss'],
  
})
export class ProjectsAdminComponent {
  success :string 
  error :string 
  @ViewChild(ProjectsTableComponent) projectsTable: ProjectsTableComponent;
  constructor(private modalService: NgbModal, private projectService: ProjectsService) { }

  open(content, type, modalDimension) {
   if (type === 'Notification') {
      this.modalService.open(content, { windowClass: 'modal-danger', centered: true });
    } else {
      this.modalService.open(content, { centered: true });
    }
  }

  onCreate(form: NgForm) {
    const projectName = form.value;
    this.projectService.addProject(projectName).subscribe({
      next: () => {
        this.projectsTable.getProjects();
        this.error = null;
        this.success = 'Project created correctly!';
        this.modalService.dismissAll();
        form.reset();
      },
      error: (e) => {
        this.success = null;
        this.error = e.error;
      }
    });
  }

}
