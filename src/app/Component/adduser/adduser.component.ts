import {  UserService } from '../../Services/Identity/User.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GroupService } from 'src/app/Services/Group/Group.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: [],
})
export class AdduserComponent {
  RegisterForm = new FormGroup({
    email: new FormControl('', Validators.required),
    userName: new FormControl('', Validators.required),
    groupid:new FormControl(),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),

  });
  Groups:any;
  constructor(private RegisterService: UserService,private GroupService:GroupService) {}

  ngOnInit() {

    this.GroupService.getAllGroup().subscribe({
      next: (result) => {
        this.Groups = result;
        console.log(this.Groups);

       // this.Setting = this.Setting;
      },
      error: (err) => { console.log(err.err); }
    });


  }



  onSubmit() {
    if (this.RegisterForm.valid) {
      console.log(this.RegisterForm.value);
      this.RegisterService.register(this.RegisterForm.value).subscribe({
        next: (response) => {
          Swal.fire({
            icon: 'success',
            title: 'User Added',
            showConfirmButton: false,
            timer: 1500,
          });
        },
        error: (error) => {
         console.log(error);

          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'InValid value!',
          });
        },
      });
    } else {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please fill in all required fields!',
      });
    }
  }
}
