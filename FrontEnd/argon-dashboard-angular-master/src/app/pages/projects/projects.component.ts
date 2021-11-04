import {Component, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProjectsService} from './projects.service';
import {ProjectListModel} from '../../models/projectListModel';


@Component({
  selector: 'app-tables',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  isFetching = false;
  loadedProjects: ProjectListModel[] = [];
  error = null;
  isEditing = false;
  constructor(private http: HttpClient, private projectService: ProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    this.loadedProjects = [];
  }

  private getProjects() {
    this.isFetching = true;
    this.projectService.getAllProjects().subscribe({
      next: (responseData) => {
          this.isFetching = false;
          this.loadedProjects = responseData;
        },
      error: (e) => {
          this.isFetching = false;
          this.error = e.status + " " + e.statusText;
        }
    });
  }

  onDelete(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe({
      next: () => {this.loadedProjects = this.loadedProjects.filter(model => model.id !== projectId); },
      error: (e) => {this.error = e.status + " " + e.statusText; }
    });
  }

  onHandleError() {
    this.error = null;
  }

  onEdit(){
    this.isEditing = true;
  }
}
