import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProjectsService} from './projects.service';
import {ProjectModel} from './projectModel';


@Component({
  selector: 'app-tables',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {
  isFetching = false;
  loadedProjects: ProjectModel[] = [];
  error = null;
  constructor(private http: HttpClient, private projectService: ProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  private getProjects() {
    this.isFetching = true;
    this.projectService.getAllProjects().subscribe(responseData => {
      this.isFetching = false;
      this.loadedProjects = responseData;
    }, error => {
      this.isFetching = false;
      this.error = error.message;
    });
  }

  onDelete(projectName: string) {
    this.projectService.deleteProject().subscribe(() => {
      this.loadedProjects = this.loadedProjects.filter(model => model.name !== projectName);
    }, error => {
      this.error = error.message;
    });
  }

  onHandleError() {
    this.error = null;
  }
}
