import { Routes } from '@angular/router';

import { LoginComponent } from '../../pages/login/login.component';
import {PageNotFoundComponent} from '../../page-not-found/page-not-found.component';

export const AuthLayoutRoutes: Routes = [
    { path: 'login',          component: LoginComponent },

];
