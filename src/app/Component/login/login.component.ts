import { Component, Renderer2} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: []
})
export class LoginComponent {
  constructor(private route:Router){ }

login_func(){
    this.route.navigateByUrl('/home');
  }
}
