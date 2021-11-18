import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProjectListModel } from 'src/app/models/projectListModel';
import { ProjectsService } from '../projects.service';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.scss']
})
export class ProjectsTableComponent implements OnInit {

  isFetching = false;
  loadedProjects: ProjectListModel[] = [];
  error = null;
  success = null;
  constructor(private http: HttpClient, private projectService: ProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    this.loadedProjects = [];
    this.error = null;
  }

  public getProjects() {
    this.isFetching = true;
    this.projectService.getAllProjects().subscribe({
      next: (responseData) => {
          this.isFetching = false;
          this.loadedProjects = responseData;
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

}
