import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  baseURL: string = 'http://localhost:5128/api/Attendance';

  constructor(private http: HttpClient) {}

  getAllEmpAttendance() 
  {
    return this.http.get(this.baseURL);
  }

  DeleteAttRecord(id: any) 
  {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  AddAtt(att: any) 
  {
    return this.http.post(this.baseURL, att);
  }
  getAttById(id: number) {
    return this.http.get(`${this.baseURL}/${id}`);
  }
  EditAtt(att: any) {
    return this.http.put(this.baseURL, att);
  }
  addAttendances(attendances : any){    
    return this.http.post(`${this.baseURL}/attendances`, attendances);
  }

}
