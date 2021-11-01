import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from './projectModel';

@Injectable({providedIn: 'root'})
export class ProjectsService {
  headersProject: HttpHeaders;

  constructor(private http: HttpClient) {
  }

  getAllProjects() {
    this.setHeader();
    return this.http.get<[ProjectModel]>('http://localhost:5000/projects', { headers: this.headersProject });
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', localStorage.getItem('userToken'));
  }

  deleteProject() {
    this.setHeader();
    return this.http.delete('http://localhost:5000/projects/7', { headers: this.headersProject }).subscribe();
  }
}
