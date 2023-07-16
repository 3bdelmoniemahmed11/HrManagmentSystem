import { HttpClient } from '@angular/common/http';
import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: []
})
export class ProfileComponent implements OnInit{
  EmployeeID:any;
  Employee:any;
  constructor(private http:HttpClient,
    private activatedRoute : ActivatedRoute,
    private route : Router,
    private employeeServices:EmployeeService
    ){
      this.EmployeeID=activatedRoute.snapshot.paramMap.get("id");
  }
  ngOnInit(): void {
    this.employeeServices.getEmpById(this.EmployeeID).subscribe({
      next:(responce)=>{this.Employee=responce},
      error:(error)=>{console.log(error)}
    });
    console.log(this.Employee);
  }


}
