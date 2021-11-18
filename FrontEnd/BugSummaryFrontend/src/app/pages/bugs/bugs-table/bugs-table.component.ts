import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ProjectModel} from '../../../models/projectModel';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import {AssignmentModel} from '../../../models/assignmentModel';
import { UserModel } from 'src/app/models/userModel';
import { EditProjectService } from '../../projects/project/editProject.service';


@Component({
  selector: 'app-bugs-table',
  templateUrl: './bugs-table.component.html',
  styleUrls: ['./bugs-table.component.scss']
})

export class BugsTableComponent {
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  @Input() bugs: BugModel[] = [];
  error = null;
  successBug = null;
  loadedUsers: UserModel[] = [];
  projectId: string;
  isFetching = false;
  bugState = 1;
  constructor(private router: Router, private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute, private modalService: NgbModal) {
  }


  private getProjectById() {
    this.isFetching = true;
    this.editService.getProjectById(this.projectId).subscribe({
      next: (responseData) => {
        this.bugs = responseData.bugs;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.error;
      },
    });
  }

  open(content, type, modalDimension) {
    if (type === 'Notification') {
      this.modalService.open(content, { windowClass: 'modal-danger', centered: true });
    } else {
      this.modalService.open(content, { centered: true });
    }
  }

  onAddBug(AddBugForm: NgForm) {
    const bug: BugModel = AddBugForm.value;
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
  
  onStateSelectionChanged(state){
    this.bugState = parseInt(state);
  }
}
