import { AuthenticationService } from './../../Services/Identity/authentication.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: [],
})
export class NavbarComponent {
  constructor(
    private router: Router,
    private AuthenticationService: AuthenticationService
  ) {}
  logout() {
    // clear any user session data here
    this.AuthenticationService.logout();
    this.router.navigateByUrl('/login');
  }
}
