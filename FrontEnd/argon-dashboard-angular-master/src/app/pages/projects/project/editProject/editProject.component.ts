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
  bugs: BugModel [];
  users: UserModel [];
  constructor(private http: HttpClient, private editService: EditProjectService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => this.projectId = params['id']);
    // this.bugs = this.project.bugs;
    // this.users = this.project.users;
  }

  onEditName() {
    const newName: string = this.editNameForm.value;
    this.editService.editProjectName(newName, this.project.id.toString())
      .subscribe(responseData => {
        this.project.name = this.editNameForm.value.name;
        this.editNameForm.reset();
    });
  }

  onHandleCreateProjectResponse() {
    this.error = null;
  }

}
