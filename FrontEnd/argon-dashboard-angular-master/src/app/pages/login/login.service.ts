import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthModel} from '../../models/authModel';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class LoginService {
  endpoint = environment.webApi_origin + '/sessions';
  constructor(private http: HttpClient) {
  }
  
  loginUser(userCredentials: any) {
    return this.http.post<AuthModel>(this.endpoint, userCredentials);
  }
}
