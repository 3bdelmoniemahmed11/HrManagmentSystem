import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-move-dept',
  templateUrl: './move-dept.component.html',
  styleUrls: []
})
export class MoveDeptComponent {
  departmentForm: FormGroup;
  department_id: number;
  Departments:any;
  constructor(
    private departmentService:DepartmentService,
    private employeeService:EmployeeService,
    private router:Router,
    private activeRoute : ActivatedRoute){}
  ngOnInit() {
    this.activeRoute.params.subscribe((params:Params) =>
    {
      this.department_id = params['id'];

    });
    this.departmentForm =new FormGroup({
      deptId: new FormControl(this.department_id),
    });
    this.departmentService.getAllDepartments().subscribe({
      next: (result) => { this.Departments=result;
      },
      error: (err) => { console.log(err.err); }
    });
  }

  onSubmit(){
   const department= this.Departments.filter((x: any) => x.id == this.department_id);
   department[0].isDeleted=true;

    this.employeeService.updateEmployeedept(this.department_id,this.departmentForm.controls['deptId'].value).subscribe({
      next: (response) => {
        this.departmentService.editDepartment(department[0]).subscribe({
          next:(res)=>{  Swal.fire({
            icon: 'success',
            title: 'Successful',
            showConfirmButton: false,
            timer: 1500,
          });
        },
          error:(err)=>{
            console.log(err);

          }
        })

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
  }
}
