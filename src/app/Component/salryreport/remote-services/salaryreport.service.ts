import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root',
})
export class SalaryreportService {
  baseUrl: string = `${environment.apiUrl}/EmployeeSalaryReport`;
  constructor(private http: HttpClient) {}
  GenerateReportSalary() {
    return this.http.get(this.baseUrl);
  }
}
