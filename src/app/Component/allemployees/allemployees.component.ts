import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import { GroupService } from 'src/app/Services/Group/Group.service';
import { AuthenticationService } from 'src/app/Services/Identity/authentication.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-allemployees',
  templateUrl: './allemployees.component.html',
  styleUrls: []
})
export class AllemployeesComponent implements OnInit{
  Employees:any;
  isLoginRoute = false;
  groupId:number;
  permisions:any[]=[];
  results:any;
  constructor(private employeeServices:EmployeeService,
    private auth:AuthenticationService,
    private groupservice:GroupService){}
  ngOnInit(): void {
    this.employeeServices.getAllEmployees().subscribe({
      next :(response)=>{this.Employees=response; console.log(response)},
      error:(error)=>{console.log(error)}
     })
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
  IsEdit(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId==3){
        if(element.pageActionId==3){
          return true;
         }
       }
    }
    return false;

  }
  IsDelete(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId==3){
        if(element.pageActionId==4){
          return true;
         }
       }
    }
    return false;

  }


  Delete( id : number) : void
  {
    Swal.fire({
      title: 'Are you sure you want to delete this Employee?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes'
    }).then((result) => {
      if (result.isConfirmed) {
        this. employeeServices.deleteEmployee(id).subscribe({
          next : () =>
          {
            this.removeDeletedExam(id);
          },
          error : (err)=>{ console.log(err)}
        });
        Swal.fire(
          'Deleted!',
          'Employee  has been deleted.',
          'success'
        )
      }
    })


  }

  removeDeletedExam(id: number): void {
    this.Employees = this.Employees.filter((emp: any) => emp.id !== id);
  }

  searchTerm: string = '';
  filtEmployees :any[];

  get filteredEmployees(): any[] {
   this.filtEmployees= this.Employees.filter((employee:any) =>
      employee.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    console.log(this.filtEmployees);
    return this.filtEmployees;
  }

}
