import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from '../../../../models/projectModel';
import {BugModel} from '../../../../models/bugModel';
import {UserModel} from '../../../../models/userModel';


@Component({
  selector: 'app-edit-project',
  templateUrl: './editProject.component.html',
  styleUrls: ['./editProject.component.scss']
})

export class ProjectEditComponent implements OnInit{
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  error = null;
  @Input() project: ProjectModel;
  bugs: BugModel [];
  users: UserModel [];
  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.bugs = this.project.bugs;
    this.users = this.project.users;
  }

  onEditName() {
    const newName: string = this.editNameForm.value;
    const headers = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
    this.http.put(`http://localhost:5000/projects/${this.project.id}`, newName, {headers: headers})
      .subscribe(responseData => {
        this.project.name = this.editNameForm.value.name;
        this.editNameForm.reset();
    });
  }
  onCreateProject() {
    // const projectName = this.createProjectForm.value;
    // this.addProjectService.addProject(projectName).subscribe({
    //   next: () => {
    //     this.projectAddedCorrectly = true;
    //     this.createProjectForm.reset();
    //   },
    //   error: (e) => {
    //     this.error = e.status + " " + e.statusText;
    //     console.log(e);
    //   }
    // });
  }

  onHandleCreateProjectResponse() {
    this.error = null;
  }

}
