import {Component, Input } from '@angular/core';
import { ProjectModel } from 'src/app/models/projectModel';


@Component({
  selector: 'app-assignments-table',
  templateUrl: './assignments-table.component.html',
  styleUrls: ['./assignments-table.component.scss']
})

export class AssignmentsTableComponent {
  @Input() project: ProjectModel = null;
  successAssignment: string;

}
