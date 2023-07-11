import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SalaryreportService {
  baseUrl: string = 'http://localhost:64093/api/EmployeeSalaryReport';
  constructor(private http: HttpClient) {}
  GenerateReportSalary() {
    return this.http.get(this.baseUrl);
  }
}
