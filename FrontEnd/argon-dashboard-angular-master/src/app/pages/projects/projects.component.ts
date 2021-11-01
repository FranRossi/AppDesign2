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
  constructor(private http: HttpClient, private projectService: ProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  private getProjects() {
    this.isFetching = true;
    this.projectService.getAllProjects().subscribe(responseData => {
      this.isFetching = false;
      this.loadedProjects = responseData;
    });
  }

  onDelete(){
    this.projectService.deleteProject().subscribe(response => {
      this.ngOnInit();
    });
  }
}
