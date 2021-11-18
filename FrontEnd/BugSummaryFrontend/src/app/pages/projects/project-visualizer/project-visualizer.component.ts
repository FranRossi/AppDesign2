import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ProjectModel} from '../../../models/projectModel';
import {EditProjectService} from '../project-edit/project-edit.service';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { UserModel } from 'src/app/models/userModel';
import { UsersService } from '../../register/users.service';
import { AssignmentsTableComponent } from '../../assignments/assignments-table/assignments-table.component';


@Component({
  selector: 'app-edit-project',
  templateUrl: './project-visualizer.component.html',
  styleUrls: ['./project-visualizer.component.scss']
})

export class ProjectVisualizerComponent implements OnInit {
  error = null;
  successUser = null;
  successBug = null;
  successProject = null;
  project: ProjectModel = null;
  loadedUsers: UserModel[] = [];
  projectId: string;
  isFetching = false;
  bugState = 1;
  constructor(private router: Router, private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute, private modalService: NgbModal, private userService: UsersService) {
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
        this.isFetching = false;
      },
      error: (e) => {
        this.isFetching = false;
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

  onStateSelectionChanged(state){
    this.bugState = parseInt(state);
  }
}
