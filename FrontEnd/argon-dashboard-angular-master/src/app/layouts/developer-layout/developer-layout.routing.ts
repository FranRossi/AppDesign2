import { Routes } from '@angular/router';

import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugViewerComponent} from '../../pages/bugs/bug-viewer/bug-viewer.component';

export const DeveloperLayoutRoutes: Routes = [
  {
    path: '',
    redirectTo: 'bugs',
    pathMatch: 'full',
  },
  { path: 'bugs',      component: BugsComponent },
  { path: 'bugs/:id',         component: BugViewerComponent}

];
