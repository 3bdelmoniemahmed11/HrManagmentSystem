import { Component, OnInit } from '@angular/core';
import { SalaryreportService } from './remote-services/salaryreport.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-salryreport',
  templateUrl: './salryreport.component.html',
  styleUrls: [],
})
export class SalryreportComponent {
  salaryReport: any[] = [];
  EmployeeName: any;
  dateValue: any;
  filteredSalaryReport: any[] = [];
  constructor(private salaryReportService: SalaryreportService) {}

  ConfirmGenerateReports() {
    Swal.fire({
      title: 'Do you want to disburse the salaries?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.GenerateReport();
        Swal.fire('Done!', 'Payment disbursed successfully.', 'success');
      }
    });
  }
  GenerateReport() {
    this.salaryReportService.GenerateReportSalary().subscribe({
      next: (response: any) => {
        this.salaryReport = response;
        this.SearchByEmployeeName();
        this.SearchByDate();
      },
    });
  }
  SearchByEmployeeName() {
    if (this.EmployeeName && this.EmployeeName.trim() !== '') {
      const searchValue = this.EmployeeName.toLowerCase();
      this.filteredSalaryReport = this.salaryReport.filter(
        (item) =>
          item.employee &&
          item.employee.name &&
          item.employee.name.toLowerCase().includes(searchValue)
      );
    } else {
      this.filteredSalaryReport = this.salaryReport;
    }
  }
  SearchByDate() {
    if (this.dateValue) {
      const searchDate = new Date(this.dateValue);
      const filteredData = this.salaryReport.filter(
        (item) =>
          item.date &&
          new Date(item.date).toDateString() === searchDate.toDateString()
      );

      if (filteredData.length > 0) {
        this.filteredSalaryReport = filteredData;
      } else {
        this.filteredSalaryReport = [];
        Swal.fire('No Data', 'No data found for the selected date.', 'info');
      }
    } else {
      this.filteredSalaryReport = this.salaryReport;
    }
  }
}
