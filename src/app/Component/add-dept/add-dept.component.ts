import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-dept',
  templateUrl: './add-dept.component.html',
  styleUrls: [],
})
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
  }
}
