import {Component} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {UsersService} from './users.service';
import {ErrorHandler} from '../../utils/errorHandler';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  error = null;
  success = null;
  constructor(private http: HttpClient, private registerService: UsersService) { }

  onCreateAccount(RegisterForm: NgForm) {
    const userData = RegisterForm.value;
    userData.role = parseInt(userData.role);
    this.registerService.addUser(userData)
    .subscribe({
      next: () => {
        this.error = null;
        this.success = 'User correctly registered!';
      },
      error: (e) => {
        this.success = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });
  }

}
