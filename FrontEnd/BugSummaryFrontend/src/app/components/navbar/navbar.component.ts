import { Component, OnInit, ElementRef } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public focus;
  public listTitles: any[];
  public location: Location;
  role = null;
  constructor(location: Location,  private element: ElementRef, private router: Router) {
    this.location = location;
  }

  private capitalize (role: string){
    return role.charAt(0).toUpperCase() + role.slice(1);
  }

  ngOnInit() {
    this.listTitles = ROUTES.filter(listTitle => listTitle);
    this.role = this.capitalize(sessionStorage.getItem('roleName'));
  }

  getTitle() {
    var title = this.location.prepareExternalUrl(this.location.path());
    if (title.charAt(0) === '#') {
        title = title.slice( 1 );
    }

    return title;
  }

  logOut() {
    sessionStorage.removeItem('userToken');
    sessionStorage.removeItem('userRole');
  }
}
