import { Routes } from '@angular/router';

import { BugReadersComponent } from '../../pages/bug-readers/bug-readers.component';
import {ProjectEditComponent} from '../../pages/projects/project-edit/project-edit.component';
import { RegisterComponent } from '../../pages/register/register.component';
import { BugEditComponent } from 'src/app/pages/bugs/bug-editor/bug-edit.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';
import { ProjectsAdminComponent } from 'src/app/pages/projects/projects-admin/projects-admin.component';

export const AdminLayoutRoutes: Routes = [
    {
      path: '',
      redirectTo: 'projects',
      pathMatch: 'full',
    },
    { path: 'bug-editor-readers',   component: BugReadersComponent },
    { path: 'projects',         component: ProjectsAdminComponent },
    { path: 'register',         component: RegisterComponent },
    { path: 'projects/:id',         component: ProjectEditComponent},
    { path: 'projects/:id/bugs/:id',         component: BugEditComponent},
    {
      path: '**',
      component: PageNotFoundComponent,
    }

];
