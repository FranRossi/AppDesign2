import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugEditComponent} from '../../pages/bugs/bug/bug-edit.component';

export const TesterLayoutRoutes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  { path: 'dashboard',      component: DashboardComponent },
  { path: 'bugs',      component: BugsComponent },
  { path: 'bugs/:id',         component: BugEditComponent}

];
