import { Component } from '@angular/core';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import { GroupService } from 'src/app/Services/Group/Group.service';
import { UserService } from 'src/app/Services/Identity/User.service';
import { AuthenticationService } from 'src/app/Services/Identity/authentication.service';
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
  groupId:number;
  permisions:any[]=[];
  results:any;
  constructor(private UserService:UserService,private employeeServices:EmployeeService,
    private auth:AuthenticationService,
    private groupservice:GroupService){}

  ngOnInit(){
 this.UserService.getAllUsers().subscribe({
      next: (result) => { this.Users=result;
console.log(this.Users);

        this.OriginUsers=result;
      },
      error: (err) => { console.log(err); }
    });
    this.auth.decodedToken();
    this.groupId = this.auth.getGroupIdFromToken();


    this.groupservice.getByID(this.groupId).subscribe({
      next:(result)=>{
        this.results= result;
        console.log(this.results);

        for (let index = 0; index < this.results.length; index++) {
          if(this.results[index].pageId==3){

          this.permisions.push(this.results[index]);
          }

        }

      },
      error:(err)=>{console.log(err);
      }
    })
  }
  //pageActionId
  IsADD(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId=3){
        if(element.pageActionId=2){
          return true;
         }
       }

    }
    return false;

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

      window.location.reload();
    }
  })


}
}
