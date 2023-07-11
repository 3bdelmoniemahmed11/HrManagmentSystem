import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AnnualVacationsService {

  baseURL: string = `${environment.apiUrl}/AnnualVacations`;

  constructor(private http: HttpClient) {}

  getAnnual() {
    return this.http.get(this.baseURL);
  }
  Add(setting: any) {
    return this.http.post(this.baseURL, setting);
  }

  Update(setting: any) {
   return this.http.put(this.baseURL, setting);
  }
}
