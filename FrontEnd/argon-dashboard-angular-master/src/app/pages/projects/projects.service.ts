import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from '../../models/projectModel';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class ProjectsService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/projects';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  getAllProjects() {
    return this.http.get<[ProjectModel]>(this.endpoint, {headers : this.headersProject});
  }

  deleteProject(projectId: number) {
    return this.http.delete(this.endpoint + `/${projectId}` , {headers : this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
