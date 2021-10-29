import { Component, OnInit, OnDestroy } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  constructor(private http: HttpClient) {}

  ngOnInit() {
  }
  ngOnDestroy() {
  }

  onSignIn(userData: {username: string; password: string}) {
    this.http.post('http://localhost:5000/users', userData).subscribe(responseData => {
      console.log(responseData);
    });
 }
}
