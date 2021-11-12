import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: 'dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '3,2,1' },
    { path: 'bug-readers', title: 'Bugs reader',  icon: 'ni-single-02 text-yellow', class: '3' },
    { path: 'projects', title: 'Projects',  icon: 'ni-bullet-list-67 text-red', class: '3' },
   // { path: 'login', title: 'Login',  icon: 'ni-key-25 text-info', class: '3,2,1' },
    { path: 'register', title: 'Register',  icon: 'ni-circle-08 text-pink', class: '3' },
    { path: 'bugs', title: 'Bugs',  icon: 'ni-circle-08 text-pink', class: '2,1' }

];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem.class.includes(sessionStorage.getItem('userRole')));
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
}
