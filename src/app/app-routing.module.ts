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
<<<<<<< HEAD
import { AuthGuard } from './guards/auth.guard';
import { MoveDeptComponent } from './Component/move-dept/move-dept.component';
=======
import { AddEditAttendanceComponent } from './Component/add-edit-attendance/add-edit-attendance.component';
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
<<<<<<< HEAD
  { path: 'home', component: HomeComponent , canActivate: [AuthGuard]},
  { path: 'profile', component: ProfileComponent,canActivate: [AuthGuard] },
  { path: 'newemployee', component: NewemployeeComponent,canActivate: [AuthGuard] },
  { path: 'addDepartment/:id', component: AddDeptComponent,canActivate: [AuthGuard] },
  { path: 'addGroup', component: AddGroupComponent,canActivate: [AuthGuard] },
  { path: 'salaryrepot', component: SalryreportComponent,canActivate: [AuthGuard] },
  { path: 'adduser', component: AdduserComponent,canActivate: [AuthGuard] },
  { path: 'empHoliday', component: EmpholidayesComponent,canActivate: [AuthGuard] },
  { path: 'officialvac', component: OfficialvacComponent,canActivate: [AuthGuard] },
  { path: 'attendance', component: AttendanceComponent,canActivate: [AuthGuard] },
  { path: 'allEmp', component: AllemployeesComponent,canActivate: [AuthGuard] },
  { path: 'showDepts', component: ShowdepartsComponent,canActivate: [AuthGuard] },
  { path: 'showUsers', component: ShowusersComponent,canActivate: [AuthGuard] },
  { path: 'Print', component: PrintFormComponent,canActivate: [AuthGuard] },
  { path: 'permission', component: PermissionComponent,canActivate: [AuthGuard] },
  { path: 'mDept/:id',component:MoveDeptComponent,canActivate:[AuthGuard]}
=======
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
>>>>>>> ccaca1490def8f848790231eeb57bc5cc5b4b560
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
