import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: []
})
export class NavbarComponent {
  constructor(private router: Router) {}
  logout() {
    // clear any user session data here
    this.router.navigateByUrl('/login');
  }
}
