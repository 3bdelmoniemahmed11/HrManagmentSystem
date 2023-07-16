import { Component, OnInit } from '@angular/core';
import { SalaryreportService } from './remote-services/salaryreport.service';
import Swal from 'sweetalert2';
import { GeneralSettingSService } from 'src/app/Services/GeneralSetting/GeneralSettings.service';
import { Observable, catchError, map, of } from 'rxjs';
import { Router } from '@angular/router';


@Component({
  selector: 'app-salryreport',
  templateUrl: './salryreport.component.html',
  styleUrls: [],
})
export class SalryreportComponent implements OnInit {
  page: number = 1;
  count: number = 0;
  tableSize: number = 7;
  tableSizes: any = [3, 6, 9, 12];

  salaryReport: any[] = [];
  EmployeeName: any;
  dateValue: any;
  filteredSalaryReport: any[] = [];

  constructor(
    private salaryReportService: SalaryreportService,
    private generalSettings: GeneralSettingSService,
    private router: Router
  ) {}

  ngOnInit() {
    this.GenerateReport();
  }
  
  convertToJSON(obj: any): string {
    return JSON.stringify(obj);
  }
  ConfirmExistingLastSettings(): Observable<boolean> {
    return this.generalSettings.getLastSetting().pipe(
      map((result) => {
        return result !== null; // Return true if the result is not null
      }),
      catchError(() => {
        return of(false); // Return false in case of an error
      })
    );
  }

  ConfirmGenerateReports() {
    this.ConfirmExistingLastSettings().subscribe((settingsExist) => {
      if (settingsExist) {
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
      } else {
        Swal.fire({
          title: 'Please insert General settings first.',
          icon: 'warning',
          showCancelButton: false,
          showConfirmButton: true,
          confirmButtonColor: '#3085d6',
          confirmButtonText: 'OK',
        });
      }
    });
  }

  GenerateReport() {
    this.salaryReportService.GenerateReportSalary().subscribe({
      next: (response: any) => {
        this.salaryReport = response;
        this.SearchByEmployeeName();
        this.SearchByDate();
        this.updateDisplayedData();
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
    this.count = this.filteredSalaryReport.length; // Update the count variable
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
    this.count = this.filteredSalaryReport.length; // Update the count variable
  }

  onTableDataChange(event: any) {
    this.page = event;
    this.updateDisplayedData();
  }

  onTableSizeChange(event: any): void {
    this.tableSize = event.target.value;
    this.page = 1;
    this.updateDisplayedData();
  }

  updateDisplayedData() {
    const startIndex = (this.page - 1) * this.tableSize;
    const endIndex = startIndex + this.tableSize;
    this.filteredSalaryReport = this.salaryReport.slice(startIndex, endIndex);
  }
  getRowNumber(index: number): number {
    return (this.page - 1) * this.tableSize + index + 1;
  }
  navigateToPrint(employee: any) {
    this.router.navigate(['/Print'], {
      state: { data: this.convertToJSON(employee) },
    });
  }
}
