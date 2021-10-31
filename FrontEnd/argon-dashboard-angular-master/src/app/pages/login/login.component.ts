import {Component, OnInit, OnDestroy, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild('formSignIn') signInForm: NgForm;
  constructor(private http: HttpClient) {}

  ngOnInit() {
  }
  ngOnDestroy() {
  }

  onSignIn() {
    const userCredentials = this.signInForm.value;
    console.log(userCredentials);
    this.http.post('http://localhost:5000/sessions', userCredentials,
      {responseType: 'text'}).subscribe(responseData => {
      console.log(responseData);
    });
 }
}
