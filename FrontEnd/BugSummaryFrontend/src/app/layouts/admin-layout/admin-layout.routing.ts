import { Routes } from '@angular/router';

import { BugReadersComponent } from '../../pages/bug-readers/bug-readers.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import {ProjectEditComponent} from '../../pages/projects/project/editProject.component';
import { RegisterComponent } from '../../pages/register/register.component';
import { BugEditComponent } from 'src/app/pages/bugs/bug-editor/bug-edit.component';

export const AdminLayoutRoutes: Routes = [
    {
      path: '',
      redirectTo: 'projects',
      pathMatch: 'full',
    },
    { path: 'bug-editor-readers',   component: BugReadersComponent },
    { path: 'projects',         component: ProjectsComponent },
    { path: 'register',         component: RegisterComponent },
    { path: 'projects/:id',         component: ProjectEditComponent},
    { path: 'projects/:id/bugs/:id',         component: BugEditComponent},

];
