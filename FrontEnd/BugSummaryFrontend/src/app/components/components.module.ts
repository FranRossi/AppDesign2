import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BugsTableComponent } from '../pages/bugs/bugs-table/bugs-table.component';
import { AssignmentsTableComponent } from '../pages/assignments/assignments-table/assignments-table.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    FormsModule
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    BugsTableComponent,
    AssignmentsTableComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    BugsTableComponent,
    AssignmentsTableComponent
  ]
})
export class ComponentsModule { }
