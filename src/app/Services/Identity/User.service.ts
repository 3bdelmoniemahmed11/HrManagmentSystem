import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseURL = `${environment.AccountUrl}`;
  constructor(private http: HttpClient) {}
  register(registerModel: any) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseURL}/AddUser`, registerModel, {
      headers,
    });
  }

  getAllUsers(){
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseURL}/GetAllUsers`, { headers });
  }
}
