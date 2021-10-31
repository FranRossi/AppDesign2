import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @ViewChild('f') createAccountForm: NgForm;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  onCreateAccount() {
    const values = this.createAccountForm.value;
    this.http.post('http://localhost:5000/users', values);
    this.createAccountForm.reset();
  }
}
