import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private baseURL = `${environment.AccountUrl}`;
  private userPayload: any;
  constructor(private http: HttpClient) {
    this.userPayload = this.decodedToken();
  }
  login(loginModel: any) {
    return this.http.post<any>(`${this.baseURL}/login`, loginModel);
  }
  register(registerModel: any) {
    return this.http.post<any>(`${this.baseURL}/AddUser`, registerModel);
  }
  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }
  getToken() {
    return localStorage.getItem('token');
  }
  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return token !== null;
  }
  logout() {
    localStorage.removeItem('token');
  }
  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    this.userPayload = jwtHelper.decodeToken(token);
    return this.userPayload;
  }

  getIdFromToken() {
    if (this.userPayload)
      return this.userPayload[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ];
  }
  getGroupIdFromToken() {
    if (this.userPayload)
      return this.userPayload[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid'
      ];
  }

  getRoleFromToken() {
    if (this.userPayload)
      return this.userPayload[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];
  }
}
