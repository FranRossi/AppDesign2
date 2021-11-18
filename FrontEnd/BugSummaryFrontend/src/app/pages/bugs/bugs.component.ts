import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {NgForm} from '@angular/forms';
import {BugModel} from '../../models/bugModel';
import {BugsService} from './bugs.service';
import {BugCriteriaModel} from '../../models/bugCriteriaModel';
import { ProjectModel } from 'src/app/models/projectModel';
import { UsersService } from '../register/users.service';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.scss']
})
export class BugsComponent implements OnInit {
  isFetching = false;
  loadedBugs: BugModel[] = [];
  loadedProjects: ProjectModel[] = [];
  error = null;
  success = null;
  role = null;

  constructor(private http: HttpClient, private bugService: BugsService, private modalService: NgbModal, private userService: UsersService) { }

  ngOnInit() {
    this.getBugs();
    this.getProjects();
    this.role = sessionStorage.getItem('roleName');
  }

  ngOnDestroy() {
    this.loadedBugs = [];
    this.error = null;
  }

  private getBugs() {
    this.isFetching = true;
    this.bugService.getAllBugs().subscribe({
      next: (responseData) => {
        this.loadedBugs = responseData;
      },
      error: (e) => {
        this.success = null;
        this.error = e.error;
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
        this.success = null;
        this.error = e.error;
      },
      complete: () =>{
        this.isFetching = false;
      }
    });
  }

  private getBugsFiltered(filterForm: NgForm) {
    this.isFetching = true;
    const filters: BugCriteriaModel = filterForm.value;
    this.bugService.getAllBugsFiltered(filters).subscribe({
      next: (responseData) => {
        filterForm.reset();
        this.modalService.dismissAll();
        this.isFetching = false;
        this.loadedBugs = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.success = null;
        this.error = e.error;
      }
    });
  }

  onHandleError() {
    this.error = null;
  }

  onCreate(form: NgForm) {
    const bug: BugModel = form.value;
    bug.id = 1;
    this.bugService.addBug(bug).subscribe({
      next: () => {
        form.reset();
        this.modalService.dismissAll();
        this.getBugs();
          this.error = null;
          this.success = 'Bug created correctly!';
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
