import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import {TesterLayoutComponent} from './layouts/tester-layout/tester-layout.component';
import {DeveloperLayoutComponent} from './layouts/developer-layout/developer-layout.component';
import {PageNotFoundComponent} from './page-not-found/page-not-found.component';
import {AuthorizationGuard} from './guards/authorization.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  }, {
    path: 'admin',
    canActivate: [AuthorizationGuard],
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('src/app/layouts/admin-layout/admin-layout.module').then(m => m.AdminLayoutModule),
      }
    ]
  }, {
    path: 'tester',
    canActivate: [AuthorizationGuard],
    component: TesterLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('src/app/layouts/tester-layout/tester-layout.module').then(m => m.TesterLayoutModule),
      }
    ]
  }, {
    path: 'developer',
    canActivate: [AuthorizationGuard],
    component: DeveloperLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('src/app/layouts/developer-layout/developer-layout.module').then(m => m.DeveloperLayoutModule),
      }
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('src/app/layouts/auth-layout/auth-layout.module').then(m => m.AuthLayoutModule)
      }
    ]
  }, {
    path: '**',
    component: PageNotFoundComponent,
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes, {
      useHash: true
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
