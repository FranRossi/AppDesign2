import {Injectable, ViewChild} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {BugReaderInfoModel} from '../../models/bugReaderModel';
import {ParameterModel} from '../../models/parameterModel';
import {environment} from '../../../environments/environment';
import {NgForm} from '@angular/forms';

@Injectable({providedIn: 'root'})

export class BugReaderService {
  @ViewChild('formSignIn') signInForm: NgForm;
  headers: HttpHeaders;
  endpoint = environment.webApi_origin + '/bugReaders';

  constructor(private http: HttpClient) {
    this.setHeader();
  }

  getAllBugReaders() {
    return this.http.get<[BugReaderInfoModel]>(this.endpoint, {headers : this.headers});
  }
  
  readBugs(parameters: BugReaderInfoModel) {
    this.setHeader();
    return this.http.post(this.endpoint, parameters, {headers : this.headers});
  }

  private setHeader() {
    this.headers = new HttpHeaders().append('token', sessionStorage.getItem('userToken'));
  }
}
