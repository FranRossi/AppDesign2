import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from '../../models/projectModel';
import {environment} from '../../../environments/environment';
import {BugModel} from '../../models/bugModel';
import {BugCriteriaModel} from '../../models/bugCriteriaModel';

@Injectable({providedIn: 'root'})
export class BugsService {
  headersProject: HttpHeaders;
  bugsFilters: HttpParams;
  endpoint = environment.webApi_origin + '/bugs';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  getAllBugs() {
    this.setHeader();
    return this.http.get<[BugModel]>(this.endpoint, {headers : this.headersProject});
  }

  deleteBug(bugId: number) {
    this.setHeader();
    return this.http.delete(this.endpoint + `/${bugId}` , {headers : this.headersProject});
  }

  addBug(bug: BugModel) {
    this.setHeader();
    return this.http.post(this.endpoint, bug, {headers: this.headersProject});
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }

  getAllBugsFiltered(filters: BugCriteriaModel) {
    this.setHeader();
    this.setBugsParamsFilter(filters);
    return this.http.get<[BugModel]>(this.endpoint, {headers : this.headersProject, params: this.bugsFilters});
  }

  private setBugsParamsFilter(filters: BugCriteriaModel) {
    this.bugsFilters = new HttpParams().append('Name', filters.name);
    this.bugsFilters.append('Id', filters.id);
    this.bugsFilters.append('State', filters.state);
    this.bugsFilters.append('ProjectId', filters.projectId);
  }
}
