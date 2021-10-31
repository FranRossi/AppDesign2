import {Component, OnInit, OnDestroy, ViewChild} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {NgForm} from '@angular/forms';
import {DashboardComponent} from '../dashboard/dashboard.component';
import {catchError} from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild('formSignIn') signInForm: NgForm;
  receivedToken = false;
  constructor(private http: HttpClient) {}

  ngOnInit() {
    if (localStorage.getItem('userToken') != null) {
     window.location.href = 'http://localhost:4200/#/dashboard';
    }
  }
  ngOnDestroy() {
  }

  onSignIn() {
    const userCredentials = this.signInForm.value;
    console.log(userCredentials);
    this.http.post ('http://localhost:5000/sessions', userCredentials, {responseType: 'text'})
      .subscribe(responseData => {
        localStorage.setItem('userToken', responseData);
        this.receivedToken = true;
      this.ngOnInit();
    });
 }
}
