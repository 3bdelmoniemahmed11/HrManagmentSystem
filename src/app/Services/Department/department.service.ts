import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  baseURL:string=`${environment.apiUrl}/Department`;
  constructor(private http:HttpClient) { }

  getAllDepartments(){
    return this.http.get(this.baseURL)
  }

  getDepartmentById(departId:any){
    return this.http.get(`${this.baseURL}/${departId}`)
  }
  addDepartment(dept:any){
    return this.http.post(this.baseURL,dept)
  }
  editDepartment(dept:any){
    return this.http.put(this.baseURL,dept)
  }
}
