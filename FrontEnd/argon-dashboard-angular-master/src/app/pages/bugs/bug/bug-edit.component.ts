import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {BugModel} from '../../../models/bugModel';
import {BugService} from './bug.service';

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
  constructor(private http: HttpClient, private bugService: BugService, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.bugId = params['id']);
    this.getBugById();
  }

  private getBugById() {
    this.isFetching = true;
    this.bugService.getBugById(this.bugId).subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.bug = responseData;
        console.log(responseData);
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

  onEditBug() {
    console.log(this.editBugForm);
    let updatedBug: BugModel = this.editBugForm.value;
    updatedBug = this.updateBugFromForm(updatedBug);
    console.log(updatedBug);
    this.bugService.editBug(updatedBug, this.bug.id.toString())
      .subscribe(responseData => {
        this.bug = updatedBug;
      });
  }

  private updateBugFromForm(updatedBug: BugModel) {
    updatedBug.id = updatedBug.id.toString() === '' ? this.bug.id : updatedBug.id;
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
