import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import { ProjectModel } from 'src/app/models/projectModel';

@Injectable({providedIn: 'root'})
export class UsersService {
  endpoint = environment.webApi_origin + '/users';
  headers: HttpHeaders;
  
  constructor(private http: HttpClient) {
  }

  addUser(userCredentials: any) {
    this.setHeader();
    return this.http.post(this.endpoint, userCredentials, {headers : this.headers});
  }

  getUserProjects() {
    this.setHeader();
    return this.http.get<[ProjectModel]>(this.endpoint, {headers : this.headers});
  }

  private setHeader() {
    this.headers = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
