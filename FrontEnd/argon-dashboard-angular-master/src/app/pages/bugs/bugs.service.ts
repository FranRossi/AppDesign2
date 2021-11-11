import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ProjectModel} from '../../models/projectModel';
import {environment} from '../../../environments/environment';
import {BugModel} from '../../models/bugModel';

@Injectable({providedIn: 'root'})
export class BugsService {
  headersProject: HttpHeaders;
  endpoint = environment.webApi_origin + '/bugs';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  getAllBugs() {
    return this.http.get<[BugModel]>(this.endpoint, {headers : this.headersProject});
  }

  deleteBug(bugId: number) {
    return this.http.delete(this.endpoint + `/${bugId}` , {headers : this.headersProject});
  }

  addBug(bugModel: BugModel) {
    this.setHeader();
    return this.http.post(this.endpoint, bugModel, {headers : this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
