import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProjectsService} from './projects.service';
import {ProjectListModel} from '../../models/projectListModel';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
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
  constructor(private http: HttpClient, private projectService: ProjectsService, private modalService: NgbModal) { }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    this.loadedProjects = [];
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
          this.error = e.status + " " + e.statusText;
        }
    });
  }

  onDelete(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe({
      next: () => {this.loadedProjects = this.loadedProjects.filter(model => model.id !== projectId); this.modalService.dismissAll(); },
      error: (e) => {this.error = e.status + " " + e.statusText; }
    });
  }

  onHandleError() {
    this.error = null;
  }

  onCreate(form: NgForm) {
    const projectName = form.value;
    form.reset();
    this.modalService.dismissAll();
    this.projectService.addProject(projectName).subscribe({
      next: () => {this.getProjects(); },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
        console.log(e);
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
