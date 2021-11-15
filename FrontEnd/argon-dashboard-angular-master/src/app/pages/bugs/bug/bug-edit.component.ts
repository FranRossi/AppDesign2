import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import {BugEditService} from './bug-edit.service';
import { UsersService } from '../../register/users.service';
import { ProjectModel } from 'src/app/models/projectModel';

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
  role = null;
  loadedProjects: ProjectModel[] = [];
  selectedProjectName: string = null;
  constructor(private http: HttpClient, private bugService: BugEditService, private route: ActivatedRoute, private modalService: NgbModal, private userService: UsersService) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.bugId = params['id']);
    this.getBugById();
    this.role = sessionStorage.getItem('roleName');
  }

  private getBugById() {
    this.isFetching = true;
    this.bugService.getBugById(this.bugId).subscribe({
      next: (responseData) => {
        this.bug = responseData;
        this.getProjects();
      },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
      },
      complete: () =>{
        this.isFetching = false;
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
        this.error = e.error;
      },
      complete: () =>{
        this.isFetching = false;
        this.getSelectedProjectName();
      }
    });
  }

  private getSelectedProjectName(){
    this.loadedProjects.forEach(project => {
      if(project.id == this.bug.projectId)
        this.selectedProjectName = project.name;
    });
  }

  onEditBug() {
    let updatedBug: BugModel = this.editBugForm.value;
    updatedBug = this.updateBugFromForm(updatedBug);
    this.bugService.editBug(updatedBug, this.bug.id.toString())
      .subscribe(responseData => {
        this.bug = updatedBug;
        this.editBugForm.resetForm();
      });
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
}
