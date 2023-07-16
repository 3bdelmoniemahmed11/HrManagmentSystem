import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class permissionService {

  private baseURL = `${environment.AccountUrl}`;

  constructor(private http: HttpClient) {}

  getAllPages() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/GetAllPages`, { headers });
  }
  getAllActions() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/GetAllActions`, { headers });
  }
}
