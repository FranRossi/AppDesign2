import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from './projectModel';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class ProjectsService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/projects';

  constructor(private http: HttpClient) {
  }

  getAllProjects() {
    this.setHeader();
    return this.http.get<[ProjectModel]>(this.endpoint, {headers : this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', localStorage.getItem('userToken'));
  }

  deleteProject(projectId: number) {
    this.setHeader();
    return this.http.delete(this.endpoint + `/${projectId}` , {headers : this.headersProject});
  }
}
