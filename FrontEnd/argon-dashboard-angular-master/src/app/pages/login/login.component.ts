import {Component, OnInit, OnDestroy, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgForm} from '@angular/forms';
import {DashboardComponent} from '../dashboard/dashboard.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild('formSignIn') signInForm: NgForm;
  token: string; // guardarlo en el local storage
  constructor(private http: HttpClient) {}

  ngOnInit() {
    if (this.token != null) {
     window.location.href = 'http://localhost:4200/#/dashboard';
    }
  }
  ngOnDestroy() {
  }

  onSignIn() {
    const userCredentials = this.signInForm.value;
    console.log(userCredentials);
    this.http.post('http://localhost:5000/sessions', userCredentials,
      {responseType: 'text'}).subscribe(responseData => {
        this.token = responseData; // guardarlo en el local storage
      console.log(responseData);
      this.ngOnInit();
      this.token = '';
    });
 }

}
