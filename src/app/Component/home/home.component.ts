import { Component } from '@angular/core';
import { NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import { UserService } from 'src/app/Services/Identity/User.service';
import { WeeklyVacationsService } from 'src/app/Services/WeeklyVacations/WeeklyVacations.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: []
})
export class HomeComponent {
  emp:any;
  dept:any;
  user:any;
  day:any;
  constructor(
    private employeeService:EmployeeService,
    private departmentService:DepartmentService,
    private weekendService:WeeklyVacationsService,
    private userService:UserService) {


  }
ngOnInit(){
  this.employeeService.getAllEmployees().subscribe({
    next:(res)=>{this.emp=res; this.emp=this.emp.length}
  });

  this.departmentService.getAllDepartments().subscribe({
    next:(res)=>{this.dept=res; this.dept=this.dept.length}
  })

  this.weekendService.GetDays().subscribe({
    next:(res)=>{this.day=res; this.day=this.day.length}
  })

  this.userService.getAllUsers().subscribe({
    next:(res)=>{this.user=res; this.user=this.user.length}
  })
}
}
