import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import {BugEditService} from './bug-edit.service';
import { UsersService } from '../../register/users.service';
import { ProjectModel } from 'src/app/models/projectModel';
import { EditProjectService } from '../../projects/project-edit/project-edit.service';
import {ErrorHandler} from '../../../utils/errorHandler';
import {BugsService} from '../bugs.service';

@Component({
  selector: 'app-bug',
  templateUrl: './bug-edit.component.html',
  styleUrls: ['./bug-edit.component.scss']
})
export class BugEditComponent implements OnInit {

  @ViewChild('f') editBugForm: NgForm;
  error = null;
  bug: BugModel = null;
  bugId: string;
  isFetching = false;
  loadedProjects: ProjectModel[] = [];
  selectedProjectName: string = null;
  constructor(private router: Router, private http: HttpClient, private editService: EditProjectService,
              private bugService: BugEditService, private route: ActivatedRoute, private modalService: NgbModal,
              private userService: UsersService, private bugDeleteService: BugsService) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.bugId = params['id']);
    this.getBugById();
  }

  private getBugById() {
    this.isFetching = true;
    this.bugService.getBugById(this.bugId).subscribe({
      next: (responseData) => {
        this.bug = responseData;
        this.getProjects();
      },
      error: (e) => {
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () =>{
        this.isFetching = false;
      }
    });
  }

  onDeleteBug( bugId: number) {
    this.modalService.dismissAll();
    this.bugDeleteService.deleteBug(bugId).subscribe({
      next: () => {
        this.error = null;
        this.modalService.dismissAll();
        this.router.navigate(['../..'] , {relativeTo: this.route});
      },
      error: (e) => {
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }

  private getProjects() {
    this.isFetching = true;
    this.userService.getUserProjects().subscribe({
      next: (responseData) => {
        this.loadedProjects = responseData;
      },
      error: (e) => {
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () => {
        this.isFetching = false;
        this.getSelectedProjectName();
      }
    });
  }

  private getSelectedProjectName(){
    this.loadedProjects.forEach(project => {
      if (project.id === this.bug.projectId)
        this.selectedProjectName = project.name;
    });
  }

  onEditBug() {
    let updatedBug: BugModel = this.editBugForm.value;
    updatedBug = this.updateBugFromForm(updatedBug);
    this.bugService.editBug(updatedBug, this.bug.id.toString())
      .subscribe({
          next: (responseData) => {
            this.bug = updatedBug;
            this.editBugForm.resetForm();
            this.router.navigate(['../..'] , {relativeTo: this.route});
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

  private updateBugFromForm(updatedBug: BugModel) {
    updatedBug.id = this.bug.id;
    updatedBug.name = updatedBug.name === '' ? this.bug.name : updatedBug.name;
    updatedBug.projectId = updatedBug.projectId.toString() === '' ? this.bug.projectId : updatedBug.projectId;
    updatedBug.version = updatedBug.version === '' ? this.bug.version : updatedBug.version;
    updatedBug.state = updatedBug.state.toString() === '' ? this.bug.state : updatedBug.state;
    updatedBug.fixingTime = updatedBug.fixingTime.toString() === '' ? this.bug.fixingTime : updatedBug.fixingTime;
    updatedBug.description = updatedBug.description === '' ? this.bug.description : updatedBug.description;
    return updatedBug;
  }
}
