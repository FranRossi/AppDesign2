import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugViewerComponent} from '../../pages/bugs/bug-viewer/bug-viewer.component';

export const DeveloperLayoutRoutes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  { path: 'dashboard',      component: DashboardComponent },
  { path: 'bugs',      component: BugsComponent },
  { path: 'bugs/:id',         component: BugViewerComponent}

];
