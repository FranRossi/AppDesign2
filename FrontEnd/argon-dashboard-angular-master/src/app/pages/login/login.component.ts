import {Component, OnInit, OnDestroy, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgForm} from '@angular/forms';
import {LoginService} from './login.service';
import {map} from 'rxjs/operators';
import {AuthModel} from '../../models/authModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild('formSignIn') signInForm: NgForm;
  receivedToken = false;
  constructor(private http: HttpClient, private loginService: LoginService) {}

  ngOnInit() {
  }
  ngOnDestroy() {
  }

  onSignIn() {
    const userCredentials = this.signInForm.value;
    this.loginService.loginUser(userCredentials)
      .subscribe(responseData => {
        console.log(responseData);
      sessionStorage.setItem('userToken', responseData.token);
      sessionStorage.setItem('userRole', responseData.role.toString());
      this.receivedToken = true;
      this.loadDashboard();
    });
  }

  private loadDashboard() {
    if (this.receivedToken) {
     window.location.href = 'http://localhost:4200/#/dashboard';
    }
  }
}
