import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private baseURL = `${environment.AccountUrl}`;

  constructor(private http: HttpClient) {}

  getAllGroup() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/GetAllGroups`, { headers });
  }
  Add(Group: any) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseURL}/AddGroup`,Group, { headers });
  }

  getByID(ID: any) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/${ID}`, { headers });
  }

  Update(setting: any) {
   return this.http.put(this.baseURL, setting);
  }
}
