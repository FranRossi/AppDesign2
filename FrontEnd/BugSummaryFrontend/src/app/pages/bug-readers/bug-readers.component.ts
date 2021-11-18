import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BugReaderService} from './bug-readers.service';
import {BugReaderInfoModel} from '../../models/bugReaderModel';
import { NgForm } from '@angular/forms';
import {ErrorHandler} from '../../utils/errorHandler';

@Component({
  selector: 'app-bug-readers',
  templateUrl: './bug-readers.component.html',
  styleUrls: ['./bug-readers.component.scss']
})
export class BugReadersComponent implements OnInit {
  isFetching = false;
  loadedBugReadersInfo: BugReaderInfoModel[] = [];
  loadedBugReader: BugReaderInfoModel = null;
  error = null;
  success = null;
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
          this.error = null;
        },
      error: (e) => {
          this.isFetching = false;
          this.success = null;
          this.error = ErrorHandler.onHandleError(e);
        }
    });
  }

  onAddBugsFromFile(BugReaderForm: NgForm) {
    const values = BugReaderForm.value;
    this.loadParameterValues(values);
    this.bugReaderService.readBugs(this.loadedBugReader).subscribe({
      next: () => {
        this.error = null;
        this.success = 'Bugs added correctly!';
        this.cleanForm(BugReaderForm);
      },
      error: (e) => {
        this.success = null;
        this.error = ErrorHandler.onHandleError(e);
      }
    });

  }

  private loadParameterValues(values: any) {
    this.loadedBugReader.parameters.forEach(parameter => {
      parameter.value = values[parameter.name];
    });
  }

  private cleanForm(BugReaderForm: NgForm) {
    this.loadedBugReader.parameters.forEach(parameter => {
      parameter.value = null;
    });
    BugReaderForm.reset();
  }

}
