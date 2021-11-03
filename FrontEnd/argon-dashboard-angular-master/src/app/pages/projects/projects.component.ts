import {Component, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProjectsService} from './projects.service';
import {ProjectModel} from './projectModel';


@Component({
  selector: 'app-tables',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  isFetching = false;
  loadedProjects: ProjectModel[] = [];
  error = null;
  constructor(private http: HttpClient, private projectService: ProjectsService) { }

  ngOnInit() {
    if (this.loadedProjects.length === 0) {
      this.getProjects();
    }
  }

  ngOnDestroy() {
    this.loadedProjects = [];
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

  onDelete(projectId: number) {
    this.projectService.deleteProject().subscribe(() => {
      this.loadedProjects = this.loadedProjects.filter(model => model.id !== projectId);
    }, error => {
      this.error = error.message;
    });
  }

  onHandleError() {
    this.error = null;
  }
}
