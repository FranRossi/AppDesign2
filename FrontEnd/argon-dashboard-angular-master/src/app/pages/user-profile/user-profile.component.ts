import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BugReaderService} from './bug-readers.service';
import {BugReaderInfoModel} from '../../models/bugReaderModel';
import { ParameterModel } from 'src/app/models/parameterModel';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  isFetching = false;
  loadedBugReadersInfo: BugReaderInfoModel[] = [];
  loadedBugReader: BugReaderInfoModel = null;
  error = null;
  selectedIndex: number = null;


  constructor(private http: HttpClient, private bugReaderService: BugReaderService) { }

  ngOnInit() {
    this.getReaders();
  }

  setIndex(index: number) {
    this.selectedIndex = index;
    this.loadedBugReader = this.loadedBugReadersInfo[index];
  }


  private getReaders() {
    this.isFetching = true;
    this.bugReaderService.getAllBugReaders().subscribe({
      next: (responseData) => {
          this.isFetching = false;
          this.loadedBugReadersInfo = responseData;
          console.log(this.loadedBugReadersInfo);
        },
      error: (e) => {
          this.isFetching = false;
          this.error = e.status + " " + e.statusText;
        }
    });
  }

  onAddBugsFromFile(BugReaderForm: NgForm){
    const values = BugReaderForm.value;
    this.loadParameterValues(values);
    this.bugReaderService.readBugs(this.loadedBugReader).subscribe({
      next: () => {
        console.log('ez');
        this.cleanForm(BugReaderForm); 
      },
      error: (e) => {
        this.error = e.status + ' ' + e.statusText;
        console.log(e);
      }
    });
    
  }

  private loadParameterValues(values: any){
    this.loadedBugReader.parameters.forEach(parameter => {
      parameter.value = values[parameter.name];
    });
  }

  private cleanForm(BugReaderForm: NgForm){
    this.loadedBugReader.parameters.forEach(parameter => {
      parameter.value = null;
    });
    BugReaderForm.reset();
  }

}
