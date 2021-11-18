import { Routes } from '@angular/router';

import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugEditComponent} from '../../pages/bugs/bug-editor/bug-edit.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';
import { ProjectsComponent } from 'src/app/pages/projects/projects.component';
import { ProjectVisualizerComponent } from 'src/app/pages/projects/project-visualizer/project-visualizer.component';

export const TesterLayoutRoutes: Routes = [
  {
    path: 'bugs',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: '',
    pathMatch: 'full',
    component: BugsComponent
  },
  { path: 'projects',      component: ProjectsComponent },
  { path: 'bugs/:id',         component: BugEditComponent},
  { path: 'projects/:id',         component: ProjectVisualizerComponent},
  { path: 'projects/:id/bugs/:id',         component: BugEditComponent},
  {
    path: '**',
    component: PageNotFoundComponent,
  }

];
