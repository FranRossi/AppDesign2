import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {RegisterService} from './register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  error = null;
  constructor(private http: HttpClient, private registerService: RegisterService) { }

  ngOnInit() {
  }

  onCreateAccount(RegisterForm: NgForm) {
    const userData = RegisterForm.value;
    userData.role = parseInt(userData.role);
    this.registerService.addUser(userData)
    .subscribe({
      next: () => {
        console.log('ez');
        this.cleanForm(RegisterForm); 
      },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
        console.log(e);
      }
    });
  }

  private cleanForm(RegisterForm: NgForm){
    RegisterForm.reset();
  }
}
