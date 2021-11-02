import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient, HttpHeaders} from '@angular/common/http';


@Component({
  selector: 'app-add-project',
  templateUrl: './addProject.component.html',
  styleUrls: ['./addProject.component.scss.css']
})

export class ProjectAddComponent {
  @ViewChild('f') createProjectForm: NgForm;
  headersProject: HttpHeaders;

  constructor(private http: HttpClient) {
  }
  onCreateProject(){
    const projectName = this.createProjectForm.value;
    this.setHeader();
    this.http.post('http://localhost:5000/projects', projectName, {headers : this.headersProject}).subscribe(() => console.log("AddedProject"));
    this.createProjectForm.reset();
  }
  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', localStorage.getItem('userToken'));
  }

}
