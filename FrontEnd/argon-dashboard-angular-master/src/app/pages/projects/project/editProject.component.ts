import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ProjectModel} from '../../../models/projectModel';
import {EditProjectService} from './editProject.service';
import {ActivatedRoute} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';


@Component({
  selector: 'app-edit-project',
  templateUrl: './editProject.component.html',
  styleUrls: ['./editProject.component.scss']
})

export class ProjectEditComponent implements OnInit {
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  error = null;
  successUser = null;
  successBug = null;
  successProject = null;
  project: ProjectModel = null;
  projectId: string;
  isFetching = false;
  constructor(private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.projectId = params['id']);
    this.getProjectById();
  }

  private getProjectById() {
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

  onEditName() {
    const newName: string = this.editNameForm.value;
    this.editService.editProjectName(newName, this.project.id.toString())
      .subscribe({
        next: () => {
        this.project.name = this.editNameForm.value.name;
        this.error = null;
        this.successProject = "Name changed correctly!";
        this.editNameForm.reset();
        },
        error: (e) => {
          this.successProject = null;
          this.error = e.error;
        }
    });
  }

  onDeleteBug( bugId: number) {
    this.modalService.dismissAll();
    this.editService.deleteBug(bugId).subscribe({
      next: () => {
        this.error = null;
        this.successBug = "Bug deleted correctly!";
        this.project.bugs = this.project.bugs.filter(model => model.id !== bugId);
      },
      error: (e) => {
        this.successBug = null;
        this.error = e.error;
      }
    });
  }

  onAddUser(form: NgForm) {
    const userId = form.value.userId;
    this.editService.addUserToProject(this.project.id, userId).subscribe({
      next: () => {
        this.error = null;
        this.successUser = "User added correctly!";
        form.reset();
        this.modalService.dismissAll();
        this.getProjectById();
      },
      error: (e) => {
        this.successUser = null;
        this.error = e.error;
      }
    });
  }


  onDeleteUser(projectId: number, userId: number) {
    this.editService.deleteUserFromProject(projectId, userId).subscribe({
      next: () => {
        this.error = null;
        this.successUser = "User deleted correctly!";
        this.project.users = this.project.users.filter(model => model.id !== userId);
        this.modalService.dismissAll();
      },
      error: (e) => {
        this.successUser = null;
        this.error = e.error;
    }
    });
  }

  onAddBug(AddBugForm: NgForm) {
    const bug: BugModel = AddBugForm.value;
    bug.projectId = this.project.id;
    bug.id = 1;
    this.editService.addBugToProject(bug).subscribe({
      next: () => {
        this.error = null;
        this.successBug = "Bug added correctly!";
        AddBugForm.reset();
        this.modalService.dismissAll();
        this.getProjectById();
      },
      error: (e) => {
        this.successBug = null;
        this.error = e.error;
      }
    });
  }

}
