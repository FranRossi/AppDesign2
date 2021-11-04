import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';

@Injectable({providedIn: 'root'})
export class AddProjectService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/projects';

  constructor(private http: HttpClient) {
  }

  addProject(projectName: any) {
    this.setHeader();
    return this.http.post(this.endpoint, projectName, {headers : this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
