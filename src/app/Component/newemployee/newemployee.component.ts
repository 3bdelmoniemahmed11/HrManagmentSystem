import { Component ,OnInit} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-newemployee',
  templateUrl: './newemployee.component.html',
  styleUrls: [],
})
export class NewemployeeComponent implements OnInit {
  EmployeeId: any;
  Employee: any;
  postEmp: any;
  Employees: any;
  Departments: any;
  timedata: any;
  date: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private employeeServices: EmployeeService,
    private departmentServices: DepartmentService
  ) {
    this.employeeServices.getAllEmployees().subscribe({
      next: (response) => {
        this.Employees = response;
        console.log(response);
      },
      error: (error) => {
        console.log(error);
      },
    });
    this.departmentServices.getAllDepartments().subscribe({
      next: (response) => {
        this.Departments = response;
        console.log(response);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.EmployeeId = params['id'];
      console.log(this.EmployeeId);

      if (this.EmployeeId != 0) {
        this.employeeServices.getEmpById(this.EmployeeId).subscribe({
          next: (response) => {
            debugger;
            this.Employee = response;
            this.getName.setValue(this.Employee.name);
            this.getNationality.setValue(this.Employee.nationality);
            this.getaddress.setValue(this.Employee.address);
            this.getgender.setValue(this.Employee.gender);
            this.getdepartment.setValue(this.Employee.DeptId);
            this.getbirth.setValue(this.Employee.birthDate);
            this.getHirdate.setValue(this.Employee.hirDate);
            this.getdepartureTime.setValue(this.Employee.departureTime);
            this.getattendanceTime.setValue(this.Employee.attendanceTime);
            this.getssn.setValue(this.Employee.ssn);
            this.getphone.setValue(this.Employee.phone);
            this.getNetSalary.setValue(this.Employee.netSalary);
            this.getid.setValue(this.Employee.id);
          },
        });
      } else {
        this.postEmp = {
          id: 0,
          nationality: '',
          gender: '',
          birthDate: '',
          ssn: '',
          phone: '',
          address: '',
          hirdate: '',
          attendanceTime: '',
          departureTime: '',
          department: '',
          departmentname: '',
          netsalary: 0,
          isDeleted: false,
        };
      }
    });
  }

  employeeForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    nationality: new FormControl('', [Validators.required]),
    gender: new FormControl('', [Validators.required]),
    birthDate: new FormControl('', [Validators.required]),
    ssn: new FormControl('', [Validators.required, Validators.minLength(14)]),
    phone: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    hirDate: new FormControl('', [Validators.required]),
    attendanceTime: new FormControl('', [Validators.required]),
    departureTime: new FormControl('', [Validators.required]),
    department: new FormControl('', [Validators.required]),
    netsalary: new FormControl('', [Validators.required]),
    id: new FormControl(),
  });
  get getid() {
    return this.employeeForm.controls['id'];
  }
  get getName() {
    return this.employeeForm.controls['name'];
  }
  get getNationality() {
    return this.employeeForm.controls['nationality'];
  }
  get getbirth() {
    return this.employeeForm.controls['birthDate'];
  }
  get getssn() {
    return this.employeeForm.controls['ssn'];
  }
  get getgender() {
    return this.employeeForm.controls['gender'];
  }
  get getphone() {
    return this.employeeForm.controls['phone'];
  }
  get getaddress() {
    return this.employeeForm.controls['address'];
  }
  get getdepartureTime() {
    return this.employeeForm.controls['departureTime'];
  }
  get getHirdate() {
    return this.employeeForm.controls['hirDate'];
  }
  get getattendanceTime() {
    return this.employeeForm.controls['attendanceTime'];
  }
  get getdepartment() {
    return this.employeeForm.controls['department'];
  }
  get getNetSalary() {
    return this.employeeForm.controls['netsalary'];
  }

  formHandler(e: any) {
    e.preventDefault();
    // For Test
    console.log(this.employeeForm.value);
    console.log(this.employeeForm.status);
    this.date = this.getHirdate.value;
    let indate = new Date(this.date);
    this.timedata = this.formatDateForInput(indate);
    this.getHirdate.setValue(this.timedata);

    //
    if (this.employeeForm.status == 'VALID') {
      // add
      if (this.EmployeeId == 0) {
        console.log(this.EmployeeId);
        console.log(this.postEmp);
        this.postEmp.id = 0;
        this.postEmp.name = this.getName.value;
        this.postEmp.nationality = this.getNationality.value;
        this.postEmp.address = this.getaddress.value;
        this.postEmp.gender = this.getgender.value;
        this.postEmp.department = this.getdepartment.value;
        this.postEmp.birthDate = this.getbirth.value;
        this.postEmp.hirdate = this.getHirdate.value;
        this.postEmp.departureTime = this.getdepartureTime.value;
        this.postEmp.attendanceTime = this.getattendanceTime.value;
        this.postEmp.ssn = this.getssn.value;
        this.postEmp.phone = this.getphone.value;
        this.postEmp.netSalary = this.getNetSalary.value;

        console.log(this.postEmp);
        this.employeeServices.addEmployee(this.postEmp).subscribe({
          next: () => {
          this.ConfirmAddEmployee();
          this.router.navigate(['/allEmp']);

          },
          error:(error)=>{console.log(error);
          }
        });
      } else {
        // this.timedata=this.formatDateForInput(this.employeeForm.controls.hirDate);

        this.employeeServices.editEmployee(this.employeeForm.value).subscribe({
          next: () => {
            console.log('ok edit');
            this.router.navigate(['/allEmp']);
          },
        });
      }
    }

    console.log(this.employeeForm.value);
    console.log(this.employeeForm);
  }

  Delete(id: number): void {
    Swal.fire({
      title: 'Are you sure you want to delete this Employee?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.employeeServices.deleteEmployee(id).subscribe({
          next: () => {
            this.removeDeletedExam(id);
          },
          error: (err) => {
            console.log(err);
          },
        });
        Swal.fire('Deleted!', 'Employee  has been deleted.', 'success');
      }
    });
  }

  removeDeletedExam(id: number): void {
    this.Employees = this.Employees.filter((emp: any) => emp.id !== id);
  }
  formatDateForInput(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
  }
  ConfirmAddEmployee() {
    Swal.fire({
      title: 'Added Successfully',
      icon: 'warning',
      showCancelButton: false,
      showConfirmButton: true,
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'OK',
    });
  }
}
