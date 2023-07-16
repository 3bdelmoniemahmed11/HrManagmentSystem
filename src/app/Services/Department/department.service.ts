import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

<<<<<<< HEAD
@Injectable({
  providedIn: 'root'
})
=======

@Injectable({
  providedIn: 'root'
})
  
  
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560
export class DepartmentService {

  baseURL:string=`${environment.apiUrl}/Department`;
  constructor(private http:HttpClient) { }
<<<<<<< HEAD

  getAllDepartments(){
    return this.http.get(this.baseURL)
  }

  getDepartmentById(departId:any){
    return this.http.get(`${this.baseURL}/${departId}`)
  }
  addDepartment(dept:any){
    return this.http.post(this.baseURL,dept)
  }
=======
  getAllDepartments(){
    return this.http.get(this.baseURL)
  }
  getDepartmentById(departId:any){
    return this.http.get(`${this.baseURL}/${departId}`)
  }
  addDepartment(dept: any) {
    return this.http.post(this.baseURL,dept)
  }
  deleteDepartment(departId:any)
  {
    return this.http.delete(`${this.baseURL}/${departId}`)
  }
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560
  editDepartment(dept:any){
    return this.http.put(this.baseURL,dept)
  }
}
