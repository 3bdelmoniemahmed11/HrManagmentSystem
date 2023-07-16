<<<<<<< HEAD
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
=======
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-dept',
  templateUrl: './add-dept.component.html',
  styleUrls: [],
})
<<<<<<< HEAD
export class AddDeptComponent {
  departmentForm: FormGroup;
  submitted:boolean=false;
  department_id: number;
  department:any;
  constructor(
    private departmentService:DepartmentService,
    private router:Router,
    private activeRoute : ActivatedRoute){}
  ngOnInit() {
    this.departmentForm =new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(2) ])
    });
    this.activeRoute.params.subscribe((params:Params) =>
    {
      this.department_id = params['id'];
      if(this.department_id !=0){
        this.departmentService.getDepartmentById(this.department_id).subscribe({
          next : (result) =>
          {
            this.department = result;
            this.departmentForm.get('name')?.setValue(this.department.name)

          },
          error : (err) => {console.log(err)}
        });
      }

    });
  }

  onSubmit(){
    this.submitted=true;
    if(this.departmentForm.invalid){return;}
    if(this.department_id==0){
    this.departmentService.addDepartment({name:this.departmentForm.get('name')?.value,isDeleted:false}).subscribe({
      next:(result) =>{
        Swal.fire({
              icon: 'success',
              title: 'Department Added',
              showConfirmButton: false,
              timer: 1500,
            });
            this.router.navigateByUrl('/showDepts');

          },
      error:(err)=>{
        console.log(err);
      }
    });
  }
  else{
    this.department.name=this.departmentForm.get('name')?.value;
    this.departmentService.editDepartment(this.department).subscribe({
      next: (res) => {
        Swal.fire({
          icon: 'success',
          title: 'Department Name Updated',
          showConfirmButton: false,
          timer: 1500,
        });
        this.router.navigateByUrl('/showDepts');
      },
      error: (e) => { }
    });
  }
=======
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
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560
  }
}
