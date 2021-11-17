import { Routes } from '@angular/router';

import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugEditComponent} from '../../pages/bugs/bug-editor/bug-edit.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';

export const TesterLayoutRoutes: Routes = [
  {
    path: '',
    redirectTo: 'bugs',
    pathMatch: 'full',
  },
  { path: 'bugs',      component: BugsComponent },
  { path: 'bugs/:id',         component: BugEditComponent},
  {
    path: '**',
    component: PageNotFoundComponent,
  }

];
