import { HttpClient } from '@angular/common/http';
import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: []
})
export class ProfileComponent implements OnInit{
  EmployeeID:any;
  Employee:any;
  department:any;
  deptid:any;
  constructor(private http:HttpClient,
    private activatedRoute : ActivatedRoute,
    private route : Router,
    private employeeServices:EmployeeService,
    private departmentServices: DepartmentService
    ){
      this.EmployeeID=activatedRoute.snapshot.paramMap.get("id");

  }
  ngOnInit(): void {
    this.employeeServices.getEmpById(this.EmployeeID).subscribe({
      next:(responce)=>{this.Employee=responce ;

        this.departmentServices.getDepartmentById(this.Employee.deptId).subscribe({
          next: (response) => {
            this.department = response;
          },
          error: (error) => {
            console.log(error);
          },
        });
        console.log(this.deptid)},

      error:(error)=>{console.log(error)}
    });





  }


}
