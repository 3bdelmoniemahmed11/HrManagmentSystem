import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environment/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneralSettingSService {

  baseURL: string = `${environment.apiUrl}/GeneralSetting`;

  constructor(private http: HttpClient) {}

  getLastSetting() {
    return this.http.get(this.baseURL);
  }
  AddSetting(setting: any) {
    return this.http.post(this.baseURL, setting);
  }

  EditSetting(setting: any) {
   return this.http.put(this.baseURL, setting);
  }
}
