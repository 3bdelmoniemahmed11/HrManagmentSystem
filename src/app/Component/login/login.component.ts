import { AuthenticationService } from './../../Services/Identity/authentication.service';
import { Component, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: [],
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });
  constructor(
    private router: Router,
    private AuthenticationService: AuthenticationService
  ) {
    if (this.AuthenticationService.isLoggedIn()) {
      //this.router.navigate(['/']);
    }
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  /*login_func() {
    this.router.navigateByUrl('/home');
  }*/
  onSubmit() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.AuthenticationService.login(this.loginForm.value).subscribe({
        next: (response) => {
          console.log(response);
          this.AuthenticationService.storeToken(response.token);
          //console.log(this.AuthenticationService.decodedToken());
         // document.location.reload();
          this.router.navigateByUrl('/home');
        },
        error: (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Unauthorized!',
          });
          console.log(error);
        },
      });
    }
  }
}
