import { Component ,OnInit} from '@angular/core';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-allemployees',
  templateUrl: './allemployees.component.html',
  styleUrls: []
})
export class AllemployeesComponent implements OnInit{
  Employees:any;
  constructor(private employeeServices:EmployeeService){}
  ngOnInit(): void {
   this.employeeServices.getAllEmployees().subscribe({
    next :(response)=>{this.Employees=response; console.log(response)},
    error:(error)=>{console.log(error)}
   })
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
