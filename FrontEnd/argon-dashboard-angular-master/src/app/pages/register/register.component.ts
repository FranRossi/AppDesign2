import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {RegisterService} from './register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  error = null;
  success = null;
  constructor(private http: HttpClient, private registerService: RegisterService) { }

  onCreateAccount(RegisterForm: NgForm) {
    const userData = RegisterForm.value;
    userData.role = parseInt(userData.role);
    this.registerService.addUser(userData)
    .subscribe({
      next: () => {
        this.error = null;
        this.success='User correctly registered!'; 
      },
      error: (e) => {
        this.success = null;
        this.error = e.error;
      }
    });
  }
}
