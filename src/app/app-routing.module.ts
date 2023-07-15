import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './Component/profile/profile.component';
import { SalryreportComponent } from './Component/salryreport/salryreport.component';
import { AddDeptComponent } from './Component/add-dept/add-dept.component';
import { AddGroupComponent } from './Component/add-group/add-group.component';

import { NewemployeeComponent } from './Component/newemployee/newemployee.component';
import { AdduserComponent } from './Component/adduser/adduser.component';
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
import { AddEditAttendanceComponent } from './Component/add-edit-attendance/add-edit-attendance.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },

  { path: 'addDepartment/:id/edit', component: AddDeptComponent },
  { path: 'addGroup', component: AddGroupComponent },
  { path: 'salaryrepot', component: SalryreportComponent },
  { path: 'adduser', component: AdduserComponent },
  { path: 'empHoliday', component: EmpholidayesComponent },
  { path: 'officialvac', component: OfficialvacComponent },
  { path: 'attendance', component: AttendanceComponent },

  { path: 'showDepts', component: ShowdepartsComponent },
  { path: 'showUsers', component: ShowusersComponent },
  { path: 'Print', component: PrintFormComponent },
  { path: 'permission', component: PermissionComponent },
  //alla
  { path: 'allEmp', component: AllemployeesComponent },
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'allEmp/:id', component: ProfileComponent },
  { path: 'newemployee/:id/edit', component: NewemployeeComponent },
  { path: 'add/:id', component: AddEditAttendanceComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
