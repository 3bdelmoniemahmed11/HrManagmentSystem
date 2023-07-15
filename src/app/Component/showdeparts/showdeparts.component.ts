import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-showdeparts',
  templateUrl: './showdeparts.component.html',
  styleUrls: [],
})
export class ShowdepartsComponent implements OnInit {
  departments: any;
  constructor(private departmentService: DepartmentService) {}

  ngOnInit(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (response) => {
        this.departments = response;
      },
      //handle error when api is not available
    });
  }
  DeleteDepartment(departmentId: number) {}


}
