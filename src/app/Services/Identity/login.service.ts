import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private baseURL = `${environment.AccountUrl}`;

  constructor(private http: HttpClient) {}

  login(loginModel: any) {
    return this.http.post<any>(`${this.baseURL}/login`, loginModel);
  }
}
