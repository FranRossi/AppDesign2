import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BugEditService} from '../bug-editor/bug-edit.service';
import {ActivatedRoute} from '@angular/router';
import {BugModel} from '../../../models/bugModel';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {EditProjectService} from '../../projects/project/editProject.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-bug-viewer',
  templateUrl: './bug-viewer.component.html',
  styleUrls: ['./bug-viewer.component.scss']
})
export class BugViewerComponent implements OnInit {
  error: string = null;
  success: string = null;
  bug: BugModel = null;
  bugId: string;
  isFetching = false;
  constructor(private http: HttpClient, private bugService: BugEditService, private route: ActivatedRoute, private modalService: NgbModal ) {
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
      },
      error: (e) => {
        this.error = e.error;
      },
      complete: () =>{
        this.isFetching = false;
      }
    });
  }

  onFixBug(bugId, form: NgForm) {
    const fixingTime: number = form.value.fixingTime;
    this.bugService.fixBug(bugId, fixingTime)
      .subscribe({
        next: () => {
          form.reset();
          this.modalService.dismissAll();
          this.error = null;
          this.getBugById();
          this.success = 'Bug fixed correctly!';
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
