import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ProjectModel} from '../../../../models/projectModel';
import {BugModel} from '../../../../models/bugModel';
import {UserModel} from '../../../../models/userModel';
import {EditProjectService} from './editProject.service';
import {ActivatedRoute} from '@angular/router';


@Component({
  selector: 'app-edit-project',
  templateUrl: './editProject.component.html',
  styleUrls: ['./editProject.component.scss']
})

export class ProjectEditComponent implements OnInit{
  @ViewChild('formEditNameProject') editNameForm: NgForm;
  error = null;
  project: ProjectModel = null;
  projectId: string;
  isFetching = false;
  constructor(private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.projectId = params['id']);
    this.getProjectById();
  }

  private getProjectById(){
    this.isFetching = true;
    this.editService.getProjectById(this.projectId).subscribe({
      next: (responseData) => {
        this.isFetching = false;
        this.project = responseData;
      },
      error: (e) => {
        this.isFetching = false;
        this.error = e.status + " " + e.statusText;
      }
    });
  }

  onEditName() {
    const newName: string = this.editNameForm.value;
    this.editService.editProjectName(newName, this.project.id.toString())
      .subscribe(responseData => {
        this.project.name = this.editNameForm.value.name;
        this.editNameForm.reset();
    });
  }

  onDeleteUser(projectId: number, userId: number){
    this.editService.deleteUserFromProject(projectId, userId).subscribe({
      next: () => {this.project.users = this.project.users.filter(model => model.id !== projectId); },
      error: (e) => {this.error = e.status + " " + e.statusText;
    }
    });
  }

  onHandleError() {
    this.error = null;
  }

}
