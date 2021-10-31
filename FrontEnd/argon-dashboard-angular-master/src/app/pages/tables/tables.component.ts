import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ProjectModel} from './projectModel';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss']
})
export class TablesComponent implements OnInit {
  loadedProjects: ProjectModel[];
  headersProject: HttpHeaders;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getProjects();
  }

  private setHeader() {
    this.headersProject = new HttpHeaders().append('token', localStorage.getItem('userToken'));
  }

  private getProjects() {
    this.setHeader();
    this.http.get<[ProjectModel]>('http://localhost:5000/projects', { headers: this.headersProject }
    ).subscribe(responseData => {
      // this.projects = responseData;
      console.log(responseData);
      this.loadedProjects = responseData;
    });
  }

}
