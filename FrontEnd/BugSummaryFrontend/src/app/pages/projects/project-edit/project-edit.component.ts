import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ProjectModel} from '../../../models/projectModel';
import {EditProjectService} from './project-edit.service';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import {AssignmentModel} from '../../../models/assignmentModel';
import { UserModel } from 'src/app/models/userModel';
import { UsersService } from '../../register/users.service';
import { ProjectsService } from '../projects.service';
import { AssignmentsTableComponent } from '../../assignments/assignments-table/assignments-table.component';
import {ErrorHandler} from '../../../utils/errorHandler';


@Component({
  selector: 'app-edit-project',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.scss']
})

export class ProjectEditComponent implements OnInit {
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  @ViewChild(AssignmentsTableComponent) assingmentsTable:AssignmentsTableComponent;
  error = null;
  successUser = null;
  successBug = null;
  successProject = null;
  project: ProjectModel = null;
  loadedUsers: UserModel[] = [];
  projectId: string;
  isFetching = false;
  bugState = 1;
  constructor(private router: Router, private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute, private modalService: NgbModal, private userService: UsersService, private projectService: ProjectsService) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.projectId = params['id']);
    this.getProjectById();
  }

  private getProjectById() {
    this.isFetching = true;
    this.editService.getProjectById(this.projectId).subscribe({
      next: (responseData) => {
        this.project = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () => {
        this.getUsers();
      }
    });
  }

  private getUsers() {
    this.isFetching = true;
    this.userService.getAllUsers().subscribe({
      next: (responseData) => {
        this.loadedUsers = responseData;
      },
      error: (e) => {
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () => {
        this.isFetching = false;
      }
    });
  }

  onDeleteProject(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe({
      next: () => {
        this.error = null;
        this.modalService.dismissAll();
        this.router.navigate(['..'] , {relativeTo: this.route});
      },
      error: (e) => {
        this.error = ErrorHandler.onHandleError(e);
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
          this.error = ErrorHandler.onHandleError(e);
        }
    });
  }

  onAddUser(form: NgForm) {
    const userId = form.value.userId;
    this.editService.addUserToProject(this.project.id, userId).subscribe({
      next: () => {
        this.error = null;
        this.successUser = 'User added correctly!';
        form.reset();
        this.modalService.dismissAll();
        this.getProjectById();
      },
      error: (e) => {
        this.successUser = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }


  onDeleteUser(projectId: number, userId: number) {
    this.editService.deleteUserFromProject(projectId, userId).subscribe({
      next: () => {
        this.error = null;
        this.successUser = 'User deleted correctly!';
        this.project.users = this.project.users.filter(model => model.id !== userId);
        this.modalService.dismissAll();
      },
      error: (e) => {
        this.successUser = null;
        this.error = ErrorHandler.onHandleError(e);
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
        this.successBug = 'Bug added correctly!';
        AddBugForm.reset();
        this.modalService.dismissAll();
        this.getProjectById();
      },
      error: (e) => {
        this.successBug = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }

  onAddAssignment(createAssignmentForm: NgForm) {
    const assignment: AssignmentModel = createAssignmentForm.value;
    assignment.projectId = this.project.id;
    assignment.id = 1;
    this.editService.addAssignmentToProject(assignment).subscribe({
      next: () => {
        this.error = null;
        this.assingmentsTable.successAssignment = 'Assignment added correctly!';
        createAssignmentForm.reset();
        this.modalService.dismissAll();
        this.getProjectById();
      },
      error: (e) => {
        this.assingmentsTable.successAssignment = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }

  onStateSelectionChanged(state){
    this.bugState = parseInt(state);
  }
}
