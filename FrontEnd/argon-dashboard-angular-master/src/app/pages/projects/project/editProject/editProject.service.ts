import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class EditProjectService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/projects/';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  editProjectName(newName: string, projectId: string ) {
    return this.http.put(this.endpoint + projectId, newName, {headers: this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
