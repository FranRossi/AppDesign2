import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { ProjectsComponent } from '../../pages/projects/projects.component';
import {ProjectAddComponent} from '../../pages/projects/project/addProject/addProject.component';
import {ProjectEditComponent} from '../../pages/projects/project/editProject/editProject.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'projects',         component: ProjectsComponent },
    { path: 'add-project',         component: ProjectAddComponent },
    { path: 'edit-project',         component: ProjectEditComponent }
];
