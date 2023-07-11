import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class WeeklyVacationsService {

  baseURL: string = `${environment.apiUrl}/WeeklyVacationServices`;

  constructor(private http: HttpClient) {}

  GetDays() {
    return this.http.get(this.baseURL);
  }
  AddDays(Days: any) {
    return this.http.post(this.baseURL, Days);
  }

  EditDays(Days: any) {
   return this.http.put(this.baseURL, Days);
  }
}
