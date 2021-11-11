import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {Injectable} from '@angular/core';
import {ProjectModel} from '../../../models/projectModel';
import {BugModel} from '../../../models/bugModel';

@Injectable({providedIn: 'root'})
export class EditProjectService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin ;

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }

  editProjectName(newName: string, projectId: string ) {
    return this.http.put(this.endpoint + '/projects/' + projectId, newName, {headers: this.headersProject});
  }

  getProjectById(projectId: string ) {
    return this.http.get<ProjectModel>(this.endpoint + '/projects/' + projectId, {headers: this.headersProject});
  }

  deleteUserFromProject(projectId: number, userId: number) {
    return this.http.delete(this.endpoint + '/projects/' + `${projectId}` + '/users/' + `${userId}`, {headers: this.headersProject});
  }

  deleteBug(bugId: number) {
    return this.http.delete(this.endpoint + '/bugs/' + `${bugId}`, {headers: this.headersProject});
  }

  addUserToProject(projectId: number, userId: number) {
    return this.http.post(this.endpoint + '/projects/' + `${projectId}` + '/users', userId, {headers: this.headersProject});
  }

  addBugToProject(bug: BugModel) {
    return this.http.post(this.endpoint + '/bugs', bug, {headers: this.headersProject});
  }
}
