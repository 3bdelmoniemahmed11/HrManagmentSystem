import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-dept',
  templateUrl: './add-dept.component.html',
  styleUrls: [],
})
export class AddDeptComponent implements OnInit {
  departmentId: number;
  department: any;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private departmentService: DepartmentService
  ) {}

  departmentForm = new FormGroup({
    Name: new FormControl('', [Validators.required, Validators.minLength(3)]),
  });
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.departmentId = params['id'];
      if (this.departmentId != 0) {
        this.departmentService.getDepartmentById(this.departmentId).subscribe({
          next: (response) => {
            this.department = response;

            this.getDepartmentName.setValue(this.department.name);
          },
        });
      } else {
        this.department = {
          id: 0,
          name: '',
          isDeleted: false,
        };
      }
    });
  }
  get getDepartmentName() {
    return this.departmentForm.controls['Name'];
  }

  formHandler(e: any) {
    e.preventDefault();
    if (this.departmentForm.status == 'VALID') {
      debugger;
      this.department.name = this.getDepartmentName.value;
      if (this.departmentId == 0) {
        this.departmentService.addDepartment(this.department).subscribe({
          next: () => {
            Swal.fire({
              title: 'Department added successfully',
              icon: 'success',
              showCancelButton: false,
              showConfirmButton: true,
              confirmButtonColor: '#3085d6',
              confirmButtonText: 'OK',
            }).then(() => {
              this.router.navigate(['/showDepts']);
            });
          },
          error: () => {
            Swal.fire({
              title: 'Error while adding  department',
              icon: 'error',
              showCancelButton: false,
              showConfirmButton: true,
              confirmButtonColor: '#3085d6',
              confirmButtonText: 'OK',
            });
          },
        });
      } else {
        console.log(this.department);
        this.departmentService.editDepartment(this.department).subscribe({
          next: () => {
            Swal.fire({
              title: 'Department updated successfully',
              icon: 'success',
              showCancelButton: false,
              showConfirmButton: true,
              confirmButtonColor: '#3085d6',
              confirmButtonText: 'OK',
            }).then(() => {
              this.router.navigate(['/showDepts']);
            });
          },
          error: () => {
            Swal.fire({
              title: 'Error while  saving department',
              icon: 'error',
              showCancelButton: false,
              showConfirmButton: true,
              confirmButtonColor: '#3085d6',
              confirmButtonText: 'OK',
            });
          },
        });
      }
    }
  }
}
