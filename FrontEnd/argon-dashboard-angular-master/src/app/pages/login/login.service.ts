import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthModel} from '../../models/authModel';

@Injectable({providedIn: 'root'})
export class LoginService {
  constructor(private http: HttpClient) {
  }
  loginUser(userCredentials: any) {
    return this.http.post<AuthModel>('http://localhost:5000/sessions', userCredentials);
  }
}
