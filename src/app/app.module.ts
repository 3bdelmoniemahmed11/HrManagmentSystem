import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Component/navbar/navbar.component';
import { ProfileComponent } from './Component/profile/profile.component';
import { ColorsComponent } from './Component/colors/colors.component';
import { SalryreportComponent } from './Component/salryreport/salryreport.component';

import { AddDeptComponent } from './Component/add-dept/add-dept.component';
import { AddGroupComponent } from './Component/add-group/add-group.component';
import { NewemployeeComponent } from './Component/newemployee/newemployee.component';
import { AdduserComponent } from './Component/adduser/adduser.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmpholidayesComponent } from './Component/empholidayes/empholidayes.component';
import { OfficialvacComponent } from './Component/officialvac/officialvac.component';
import { HomeComponent } from './Component/home/home.component';
import { AttendanceComponent } from './Component/attendance/attendance.component';
import { AllemployeesComponent } from './Component/allemployees/allemployees.component';
import { ShowdepartsComponent } from './Component/showdeparts/showdeparts.component';
import { ShowusersComponent } from './Component/showusers/showusers.component';
import { PrintFormComponent } from './Component/print-form/print-form.component';
import { LoginComponent } from './Component/login/login.component';
import { PermissionComponent } from './Component/permission/permission.component';
import { HttpClientModule } from '@angular/common/http';
import { MoveDeptComponent } from './Component/move-dept/move-dept.component';

import { NgxPaginationModule } from 'ngx-pagination';
import { AddEditAttendanceComponent } from './Component/add-edit-attendance/add-edit-attendance.component';
import { SidebarComponent } from './Component/sidebar/sidebar.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProfileComponent,
    ColorsComponent,
    SalryreportComponent,
    AddDeptComponent,
    AddGroupComponent,
    NewemployeeComponent,
    AdduserComponent,
    EmpholidayesComponent,
    OfficialvacComponent,
    HomeComponent,
    AttendanceComponent,
    AllemployeesComponent,
    ShowdepartsComponent,
    ShowusersComponent,
    PrintFormComponent,
    LoginComponent,
    PermissionComponent,
    MoveDeptComponent,
    AddEditAttendanceComponent,
    SidebarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxPaginationModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
