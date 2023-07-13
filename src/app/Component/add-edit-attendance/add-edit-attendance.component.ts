import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AttendanceService } from 'src/app/Services/Attendance/attendance.service';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-edit-attendance',
  templateUrl: './add-edit-attendance.component.html',
  styleUrls: ['./add-edit-attendance.component.css'],
})
export class AddEditAttendanceComponent implements OnInit {
  allEmployee: any;
  attId: any;
  attendanceById: any;

  attendance: {
    id: any;
    // isDeleted : boolean  ,
    attendanceTime: any;
    departureTime: any;
    date: any;
    empId: any;
  } = { id: 0, attendanceTime: '', departureTime: '', date: '', empId: '' };

  constructor(
    private empService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private attService: AttendanceService,
    private router: Router
  ) {}

  formatDateForInput(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
  }

  ngOnInit(): void {
    this.empService.getAllEmployees().subscribe({
      next: (res) => {
        this.allEmployee = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
    this.activatedRoute.params.subscribe((params: Params) => {
      this.attId = params['id'];
    });
    if (this.attId == 0) {
    } else {
      this.attService.getAttById(this.attId).subscribe({
        next: (res: any) => {
          this.attendanceById = res;
          this.getEmployeeId.setValue(this.attendanceById.empId);
          this.getAtttendanceTime.setValue(this.attendanceById.attendanceTime);
          this.getDepartureTime.setValue(this.attendanceById.departureTime);
          let dateOf = new Date(this.attendanceById.date);
          let formateddate = this.formatDateForInput(dateOf);
          this.getAttendanceDate.setValue(formateddate);
        },
      });
    }
  }

  AttendanceForm = new FormGroup({
    EmployeeId: new FormControl('', [Validators.required]),
    AtttendanceTime: new FormControl('', [Validators.required]),
    DepartureTime: new FormControl('', [Validators.required]),
    AttendanceDate: new FormControl('', [Validators.required]),
  });

  departureTimeValidator(): boolean {
    const attendanceTime = this.getAtttendanceTime.value;
    const departureTime = this.getDepartureTime.value;

    if (attendanceTime && departureTime && attendanceTime >= departureTime) {
      return true;
    }

    return false;
  }

  get getEmployeeId() {
    return this.AttendanceForm.controls.EmployeeId;
  }
  get getAtttendanceTime() {
    return this.AttendanceForm.controls.AtttendanceTime;
  }

  get getDepartureTime() {
    return this.AttendanceForm.controls.DepartureTime;
  }
  get getAttendanceDate() {
    return this.AttendanceForm.controls.AttendanceDate;
  }

  Add() {
    this.AttendanceForm.markAllAsTouched();

    console.log('add function');
    console.log(this.AttendanceForm);
    if (this.AttendanceForm.valid) {
      console.log('model is valid');

      if (this.attId == 0) {
        this.attendance.attendanceTime = this.getAtttendanceTime.value;
        this.attendance.departureTime = this.getDepartureTime.value;
        this.attendance.date = this.getAttendanceDate.value;
        this.attendance.empId = this.getEmployeeId.value;
        // this.attendance.isDeleted=false;
        console.log(this.attendance);
        console.log(typeof this.attendance.date);

        console.log(this.attendance.date);

        this.attService.AddAtt(this.attendance).subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.log(err);
          },
        });

        this.AttendanceForm.reset();
      } else {
        Swal.fire({
          title: 'Are you sure you want to edit this Attendance record?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes',
        }).then((result) => {
          if (result.isConfirmed) {
            this.attendanceById.attendanceTime = this.getAtttendanceTime.value;
            this.attendanceById.departureTime = this.getDepartureTime.value;
            this.attendanceById.date = this.getAttendanceDate.value;
            this.attendanceById.empId = this.getEmployeeId.value;

            this.attService.EditAtt(this.attendanceById).subscribe({
              next: (res) => {
                console.log('result' + res);
              },
            });

            Swal.fire(
              'success!',
              'Your Attendance record has been edit.',
              'success'
            );
            this.router.navigate(['attendance']);
          }
        });
      }
    }
  }
}
