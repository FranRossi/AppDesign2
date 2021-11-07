import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ProjectModel} from '../../../models/projectModel';
import {EditProjectService} from './editProject.service';
import {ActivatedRoute} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-edit-project',
  templateUrl: './editProject.component.html',
  styleUrls: ['./editProject.component.scss']
})

export class ProjectEditComponent implements OnInit{
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  error = null;
  project: ProjectModel = null;
  projectId: string;
  isFetching = false;
  constructor(private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.projectId = params['id']);
    this.getProjectById();
  }

  private getProjectById(){
    this.isFetching = true;
    this.editService.getProjectById(this.projectId).subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.project = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

  onEditName() {
    const newName: string = this.editNameForm.value;
    this.editService.editProjectName(newName, this.project.id.toString())
      .subscribe(responseData => {
        this.project.name = this.editNameForm.value.name;
        this.editNameForm.reset();
    });
  }

  onDeleteUser(projectId: number, userId: number) {
    this.editService.deleteUserFromProject(projectId, userId).subscribe({
      next: () => {this.project.users = this.project.users.filter(model => model.id !== userId); this.modalService.dismissAll(); },
      error: (e) => {this.error = e.status + ' ' + e.statusText;
    }
    });
  }

  onHandleError() {
    this.error = null;
  }

  open(content, type, modalDimension) {
    if (type === 'Notification') {
      this.modalService.open(content, { windowClass: 'modal-danger', centered: true });
    } else {
      this.modalService.open(content, { centered: true });
    }
  }

  onDeleteBug(projectId: number, bugId: number){
    this.editService.deleteBug(projectId, bugId).subscribe({
      next: () => {this.project.bugs = this.project.bugs.filter(model => model.id !== projectId); },
      error: (e) => {this.error = e.status + ' ' + e.statusText;
      }
    });
  }

  onAddUser(form: NgForm) {
    const userId = form.value.userId;
    form.reset();
    this.modalService.dismissAll();
    this.editService.addUserToProject(this.project.id, userId).subscribe({
      next: () => {this.getProjectById(); },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

}
