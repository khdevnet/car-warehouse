import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn = false;
  constructor( private location: Location, private authenticationService: AuthenticationService){}

  ngOnInit()
  {
    this.authenticationService.currentUser.subscribe((user)=>{
    this.isLoggedIn = user !== null;
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    this.authenticationService.logout();
    this.isLoggedIn = false;
    window.location.reload();
  }
}
