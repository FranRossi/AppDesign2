import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BugEditService} from '../bug-editor/bug-edit.service';
import {ActivatedRoute} from '@angular/router';
import {BugModel} from '../../../models/bugModel';;
import {EditProjectService} from '../../projects/project/editProject.service';
import {ProjectModel} from '../../../models/projectModel';

@Component({
  selector: 'app-bug-viewer',
  templateUrl: './bug-viewer.component.html',
  styleUrls: ['./bug-viewer.component.scss']
})
export class BugViewerComponent implements OnInit {
  error = null;
  bug: BugModel = null;
  bugId: string;
  isFetching = false;
  projectName: string = null;
  constructor(private http: HttpClient, private bugService: BugEditService, private route: ActivatedRoute, private editService: EditProjectService) {
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
        this.error = e.status + ' ' + e.statusText;
      },
      complete: () =>{
        this.isFetching = false;
        this.getProjectById();
      }
    });
  }

  private getProjectById() {
    this.isFetching = true;
    this.editService.getProjectById(this.bug.projectId.toString()).subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.projectName = responseData.name;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + ' ' + e.statusText;
      }
    });
  }

}
