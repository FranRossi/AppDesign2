import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { BugReadersComponent } from '../../pages/bug-readers/bug-readers.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import {ProjectEditComponent} from '../../pages/projects/project/editProject.component';
import { RegisterComponent } from '../../pages/register/register.component';
import {BugViewerComponent} from '../../pages/bugs/bug-viewer/bug-viewer.component';
import { BugEditComponent } from 'src/app/pages/bugs/bug-editor/bug-edit.component';

export const AdminLayoutRoutes: Routes = [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full',
    },
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'bug-editor-readers',   component: BugReadersComponent },
    { path: 'projects',         component: ProjectsComponent },
    { path: 'register',         component: RegisterComponent },
    { path: 'projects/:id',         component: ProjectEditComponent},
    { path: 'projects/:id/bugs/:id',         component: BugEditComponent},

];
