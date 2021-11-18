import { Routes } from '@angular/router';

import {BugsComponent} from '../../pages/bugs/bugs.component';
import {BugViewerComponent as BugVisualizerComponent} from '../../pages/bugs/bug-visualizer/bug-visualizer.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';
import { ProjectsComponent } from 'src/app/pages/projects/projects.component';
import { ProjectVisualizerComponent } from 'src/app/pages/projects/project-visualizer/project-visualizer.component';

export const DeveloperLayoutRoutes: Routes = [
  {
    path: 'bugs',
    redirectTo: "",
    pathMatch: 'full',
  },
  {
    path: '',
    pathMatch: 'full',
    component: BugsComponent
  },
  { path: 'projects',      component: ProjectsComponent },
  { path: 'bugs/:id',         component: BugVisualizerComponent},
  { path: 'projects/:id',         component: ProjectVisualizerComponent},
  { path: 'projects/:id/bugs/:id',         component: BugVisualizerComponent},
  {
    path: '**',
    component: PageNotFoundComponent,
  }
];
