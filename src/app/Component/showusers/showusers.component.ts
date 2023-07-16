import { Component } from '@angular/core';
import { UserService } from 'src/app/Services/Identity/User.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-showusers',
  templateUrl: './showusers.component.html',
  styleUrls: []
})
export class ShowusersComponent {
  Users:any;
  searchTerm:string;
  OriginUsers:any;

  constructor(private UserService:UserService){}

  ngOnInit(){
 this.UserService.getAllUsers().subscribe({
      next: (result) => { this.Users=result;

        this.OriginUsers=result;
      },
      error: (err) => { console.log(err.err); }
    });
}




DeleteUser(department:any,e:Event){
  e.preventDefault();
  Swal.fire({
    title: 'Are you sure you want to delete this annual vacation?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes'
  }).then((result) => {
    if (result.isConfirmed) {
      department.isDeleted=true;
      // this.UserService.editDepartment(department).subscribe({
      //   next: (res) => {
      //     Swal.fire({
      //       icon: 'success',
      //       title: 'Department Deleted',
      //       showConfirmButton: false,
      //       timer: 1500,
      //     });
      //   },
      //   error: (e) => { }
      // });
      window.location.reload();
    }
  })


}
}
