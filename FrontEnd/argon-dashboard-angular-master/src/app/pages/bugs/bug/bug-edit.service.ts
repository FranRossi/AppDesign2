import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {Injectable} from '@angular/core';
import {BugModel} from '../../../models/bugModel';

@Injectable({providedIn: 'root'})
export class BugEditService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/bugs/';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }

  editBug(bug: BugModel, bugId: string) {
    this.setHeader();
    return this.http.put(this.endpoint  + bugId, bug, {headers: this.headersProject});
  }

  getBugById(bugId: string ) {
    this.setHeader();
    return this.http.get<BugModel>(this.endpoint + bugId, {headers: this.headersProject});
  }
}
