import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProjectListModel } from 'src/app/models/projectListModel';
import { UsersService } from '../../register/users.service';
import {ErrorHandler} from '../../../utils/errorHandler';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.scss']
})
export class ProjectsTableComponent implements OnInit {

  isFetching = false;
  loadedProjects: ProjectListModel[] = [];
  error = null;
  success = null;
  constructor(private http: HttpClient, private userService: UsersService) { }

  ngOnInit() {
    this.getProjects();
  }

  ngOnDestroy() {
    this.loadedProjects = [];
    this.error = null;
  }

  public getProjects() {
    this.isFetching = true;
    this.userService.getUserProjects().subscribe({
      next: (responseData) => {
        this.loadedProjects = responseData;
      },
      error: (e) => {
        this.success = null;
        this.error = ErrorHandler.onHandleError(e);
      },
      complete: () => {
        this.isFetching = false;
      }
    });
  }

}
