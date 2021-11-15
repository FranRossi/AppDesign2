import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import { ProjectModel } from 'src/app/models/projectModel';
import { UserModel } from 'src/app/models/userModel';

@Injectable({providedIn: 'root'})
export class UsersService {
  usersEndpoint = environment.webApi_origin + '/users';
  projectUserEndpoint = environment.webApi_origin + '/projects/users';
  headers: HttpHeaders;
  
  constructor(private http: HttpClient) {
  }

  addUser(userCredentials: any) {
    this.setHeader();
    return this.http.post(this.usersEndpoint, userCredentials, {headers : this.headers});
  }

  getUserProjects() {
    this.setHeader();
    return this.http.get<[ProjectModel]>(this.projectUserEndpoint, {headers : this.headers});
  }

  getAllUsers() {
    this.setHeader();
    return this.http.get<[UserModel]>(this.usersEndpoint, {headers : this.headers});
  }

  private setHeader() {
    this.headers = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
