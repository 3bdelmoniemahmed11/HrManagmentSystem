import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root',
})

export class EmployeeService {
  baseURL: string =
    `${environment.apiUrl}/Employee`;
  constructor(private http: HttpClient) {}
  getAllEmployees() {
    return this.http.get(this.baseURL);
  }
  getEmpById(employeeId: any) {
    return this.http.get(`${this.baseURL}/${employeeId}`);
  }
  addEmployee(employee: any) {
    debugger
    return this.http.post(this.baseURL, employee);
  }
  deleteEmployee(employeeId: any) {
    return this.http.delete(`${this.baseURL}/${employeeId}`);
  }
  editEmployee(employee: any) {
    return this.http.put(this.baseURL, employee);
  }
  getEmpBydept(depId: any) {
    return this.http.get(`${this.baseURL}/${depId}/getByDepartment`);
  }
  updateEmployeedept(oldDepId: any,newDepId:any) {
    return this.http.put(`${this.baseURL}/${oldDepId}/${newDepId}`,oldDepId,newDepId);
  }
}
