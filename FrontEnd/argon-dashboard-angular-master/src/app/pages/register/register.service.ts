import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class RegisterService {
  endpoint = environment.webApi_origin + '/users';
  headers: HttpHeaders;
  
  constructor(private http: HttpClient) {
  }

  addUser(userCredentials: any) {
    this.setHeader();
    console.log(userCredentials);
    return this.http.post(this.endpoint, userCredentials, {headers : this.headers});
  }

  private setHeader() {
    this.headers = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
