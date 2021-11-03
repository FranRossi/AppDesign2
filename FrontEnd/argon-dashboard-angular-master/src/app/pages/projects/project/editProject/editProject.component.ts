import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from '../../projectModel';


@Component({
  selector: 'app-edit-project',
  templateUrl: './editProject.component.html',
  styleUrls: ['./editProject.component.scss']
})

export class ProjectEditComponent {
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  error = null;
  @Input() project: ProjectModel;
  constructor(private http: HttpClient) {
  }

  onEdit() {
    const newName = this.editNameForm.value;
    console.log("Llego al edit con este valor : " + newName);

    const headers = new HttpHeaders().append('token', localStorage.getItem('userToken'));
    this.http.put(`http://localhost:5000/projects/${this.project.id}`, newName, {headers: headers})
      .subscribe(responseData => {
      this.editNameForm.reset();
      console.log("Se edito bien el nombre");
      this.project.name = newName;
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
