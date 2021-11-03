import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {AddProjectService} from './addProject.service';


@Component({
  selector: 'app-add-project',
  templateUrl: './addProject.component.html',
  styleUrls: ['./addProject.component.scss.css']
})

export class ProjectAddComponent {
  @ViewChild('f') createProjectForm: NgForm;
  projectAddedCorrectly = false;
  error = null;

  constructor(private http: HttpClient, private addProjectService: AddProjectService) {
  }

  onCreateProject() {
    const projectName = this.createProjectForm.value;
    this.addProjectService.addProject(projectName).subscribe({
      next: () => {
        this.projectAddedCorrectly = true;
        this.createProjectForm.reset();
      },
      error: (e) => {
        this.error = e.message;
        console.log(e);
      }
    });
  }

  onHandleCreateProjectResponse() {
    this.error = null;
    this.projectAddedCorrectly = false;
  }

}
