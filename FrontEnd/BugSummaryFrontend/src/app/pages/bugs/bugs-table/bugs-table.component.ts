import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import { UserModel } from 'src/app/models/userModel';
import {BugCriteriaModel} from '../../../models/bugCriteriaModel';
import {ErrorHandler} from '../../../utils/errorHandler';
import {BugsService} from '../bugs.service';
import {ProjectModel} from '../../../models/projectModel';
import {UsersService} from '../../register/users.service';


@Component({
  selector: 'app-bugs-table',
  templateUrl: './bugs-table.component.html',
  styleUrls: ['./bugs-table.component.scss']
})

export class BugsTableComponent implements OnInit{
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  @Input() bugs: BugModel[] = [];
  loadedProjects: ProjectModel[] = [];
  error = null;
  successBug = null;
  loadedUsers: UserModel[] = [];
  projectId: string;
  isFetching = false;
  bugState = 1;
  constructor (private router: Router, private http: HttpClient, private bugService: BugsService, private route: ActivatedRoute, private modalService: NgbModal, private userService: UsersService) {
  }

  ngOnInit() {
    this.getProjects();
  }

  open(content, type, modalDimension) {
      this.modalService.open(content, { centered: true });
  }


  private getBugsFiltered(filterForm: NgForm) {
    this.isFetching = true;
    const filters: BugCriteriaModel = filterForm.value;
    this.bugService.getAllBugsFiltered(filters).subscribe({
      next: (responseData) => {
        filterForm.reset();
        this.modalService.dismissAll();
        this.isFetching = false;
        this.bugs = responseData;
        console.log(this.bugs);
      },
      error: (e) => {
        this.isFetching = false;
        this.successBug = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }

  onStateSelectionChanged(state){
    this.bugState = parseInt(state);
  }

  private getProjects() {
    this.isFetching = true;
    this.userService.getUserProjects().subscribe({
      next: (responseData) => {
        this.loadedProjects = responseData;
        this.modalService.dismissAll();
      },
      error: (e) => {
        this.successBug = null;
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () =>{
        this.isFetching = false;
      }
    });
  }


}
