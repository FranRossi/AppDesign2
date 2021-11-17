import {Component, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProjectsService} from './projects.service';
import {ProjectListModel} from '../../models/projectListModel';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {NgForm} from '@angular/forms';


@Component({
  selector: 'app-tables',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  isFetching = false;
  loadedProjects: ProjectListModel[] = [];
  error = null;
  success = null;
  constructor(private http: HttpClient, private projectService: ProjectsService, private modalService: NgbModal) { }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    this.loadedProjects = [];
    this.error = null;
  }

  private getProjects() {
    this.isFetching = true;
    this.projectService.getAllProjects().subscribe({
      next: (responseData) => {
          this.isFetching = false;
          this.loadedProjects = responseData;
        },
      error: (e) => {
          this.isFetching = false;
          this.success = null;
          this.error = e.error;
        }
    });
  }

  onDelete(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe({
      next: () => {
        this.error = null;
        this.success = 'Project deleted correctly!';
        this.loadedProjects = this.loadedProjects.filter(model => model.id !== projectId);
        this.modalService.dismissAll();
      },
      error: (e) => {
        this.success = null;
        this.error = e.error;
      }
    });
  }

  onHandleError() {
    this.error = null;
  }

  onCreate(form: NgForm) {
    const projectName = form.value;
    this.projectService.addProject(projectName).subscribe({
      next: () => {
        this.getProjects();
        this.error = null;
        this.success = 'Project deleted correctly!';
        this.modalService.dismissAll();
        form.reset();
      },
      error: (e) => {
        this.success = null;
        this.error = e.error;
      }
    });
  }

  open(content, type, modalDimension) {
   if (type === 'Notification') {
      this.modalService.open(content, { windowClass: 'modal-danger', centered: true });
    } else {
      this.modalService.open(content, { centered: true });
    }
  }

}
