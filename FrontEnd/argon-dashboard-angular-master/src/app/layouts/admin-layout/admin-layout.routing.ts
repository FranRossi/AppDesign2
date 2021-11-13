import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { BugReadersComponent } from '../../pages/user-profile/bug-readers.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import {ProjectEditComponent} from '../../pages/projects/project/editProject.component';
import { RegisterComponent } from '../../pages/register/register.component';

export const AdminLayoutRoutes: Routes = [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full',
    },
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'bug-readers',   component: BugReadersComponent },
    { path: 'projects',         component: ProjectsComponent },
    { path: 'register',         component: RegisterComponent },
    { path: 'projects/:id',         component: ProjectEditComponent}
];
