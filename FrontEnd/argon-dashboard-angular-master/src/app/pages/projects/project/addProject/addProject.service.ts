import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';

@Injectable({providedIn: 'root'})
export class AddProjectService {
  headersProject: HttpHeaders;

  constructor(private http: HttpClient) {
  }

  addProject(projectName: any) {
    this.setHeader();
    return this.http.post('http://localhost:5000/projects', projectName, {headers : this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', localStorage.getItem('userToken'));
  }
}
