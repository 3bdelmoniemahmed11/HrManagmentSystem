import { Component, OnInit } from '@angular/core';
import { SalaryreportService } from './remote-services/salaryreport.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-salryreport',
  templateUrl: './salryreport.component.html',
  styleUrls: [],
})
export class SalryreportComponent {
  salaryReport: any;
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
      next: (response) => {
        this.salaryReport = response;
      },
    });
  }
}
