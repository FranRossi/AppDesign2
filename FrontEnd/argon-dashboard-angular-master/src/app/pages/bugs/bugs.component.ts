import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {NgForm} from '@angular/forms';
import {BugModel} from '../../models/bugModel';
import {BugsService} from './bugs.service';
import {BugCriteriaModel} from '../../models/bugCriteriaModel';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.scss']
})
export class BugsComponent implements OnInit {
  isFetching = false;
  loadedBugs: BugModel[] = [];
  error = null;

  constructor(private http: HttpClient, private bugService: BugsService, private modalService: NgbModal) { }

  ngOnInit() {
    this.getBugs();
  }

  ngOnDestroy() {
    this.loadedBugs = [];
    this.error = null;
  }

  private getBugs() {
    this.isFetching = true;
    this.bugService.getAllBugs().subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.loadedBugs = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

  private getBugsFiltered(filterForm: NgForm) {
    this.isFetching = true;
    const filters: BugCriteriaModel = filterForm.value;
    filterForm.reset();
    this.modalService.dismissAll();
    this.bugService.getAllBugsFiltered(filters).subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.loadedBugs = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

  onDelete(bugId: number) {
    this.bugService.deleteBug(bugId).subscribe({
      next: () => {this.loadedBugs = this.loadedBugs.filter(model => model.id !== bugId); this.modalService.dismissAll(); },
      error: (e) => {this.error = e.status + " " + e.statusText; }
    });
  }

  onHandleError() {
    this.error = null;
  }

  onCreate(form: NgForm) {
    const bug: BugModel = form.value;
    bug.id = 1;
    form.reset();
    this.modalService.dismissAll();
    this.bugService.addBug(bug).subscribe({
      next: () => {this.getBugs(); },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
        console.log(e);
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
