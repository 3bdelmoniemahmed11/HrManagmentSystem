import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoginRoute = false;

  constructor(private router: Router) {
    router.events.pipe(
      filter((event: any) => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.isLoginRoute = event.url === '/login';
      }
    });
  }
}
