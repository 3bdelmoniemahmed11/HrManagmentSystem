using HrManagment.BLL.Services.AnnualVacationServices;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.EmployeeSalaryReportServices.DTOs;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.BLL.Services.GeneralSettingsServices;
using HrManagment.BLL.Services.SalaryClickLogServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.EmployeeSalaryReport
{
    public class EmployeeSalaryReportService : IEmployeeSalaryReportService
    {
        private readonly IGenericRepository<OldSalary> _oldSalaryRepository;
        private readonly IAttendanceService _attendanceService;
        private readonly ISalaryClickLogService _salaryClickService;
        private readonly IWeeklyVacationService _weeklyVacationService;
        private readonly IAnnualVacationService _annualVacationService;
        private readonly IEmployeeService _employeeService;
        private readonly IGeneralSettingsService _generalSettingsService;

        public EmployeeSalaryReportService
        (   
            IGenericRepository<OldSalary> oldSalaryRepository,
            IAttendanceService attendanceService,
            ISalaryClickLogService salaryClickService,
            IWeeklyVacationService weeklyVacationService,
            IAnnualVacationService annualVacationService,
            IEmployeeService employeeService,
            IGeneralSettingsService generalSettingsService
        )
        {
            _oldSalaryRepository = oldSalaryRepository;
            _attendanceService = attendanceService;
            _salaryClickService = salaryClickService;
            _weeklyVacationService = weeklyVacationService;
            _annualVacationService = annualVacationService;
            _employeeService = employeeService;
            _generalSettingsService = generalSettingsService;
        }
        public async Task InsertAsyn(OldSalary oldSalary)
        {
            await _oldSalaryRepository.InsertAsync(oldSalary);
             await _oldSalaryRepository.SaveAsync();
        }

        public async Task<int> CalcAttendanceDays(int EmployeeId)
        {
            var startDate = await _salaryClickService.GetStartDate();
            var actualStartDate = startDate.AddDays(1);
            var endDate = DateTime.Now;
            var NumberOfAttendanceDays = await _attendanceService.GetEmployeeAttendanceDays(EmployeeId, actualStartDate.Date, endDate.Date);
            return NumberOfAttendanceDays.Count();
        }

        public async Task<int> CalcOffDays(int EmployeeId)
        {
            var startDate = await _salaryClickService.GetStartDate();
            var endDate = DateTime.Now;

            var numOfweeklyVacationDays = await _weeklyVacationService.GetNumberOfWeeklyVacationsByPeriod(startDate, endDate);
            var annualVacationDays = await _annualVacationService.GetAnnualVacationsByPeriod(startDate, endDate);
            var numOfAnnualVacationDays = annualVacationDays.Count();
            var requiredWorkDays =  (endDate.Date - startDate.Date).Days - (numOfweeklyVacationDays + numOfAnnualVacationDays);
            var numAttendanceDays = await CalcAttendanceDays(EmployeeId);
            var offDays = requiredWorkDays - numAttendanceDays;
            return offDays;
        }

        public async Task<double> CalcBounsHours(int EmployeeId,TimeSpan DefaultAttendanceTime,TimeSpan DefaultDepartureTime )
        {
            var bounsHours=0.00;
            var actualWorkingHours = 0.0;
            var startDate = await _salaryClickService.GetStartDate();
            var endDate = DateTime.Now;
           
            var attendanceEmployeeRecrords = await _attendanceService.GetEmployeeAttendanceDays(EmployeeId, startDate.Date, endDate.Date);
            var workingHoursPerDay = (DefaultDepartureTime - DefaultAttendanceTime).TotalHours;
            TimeSpan workingHours;
            foreach (var attendanceRecord in attendanceEmployeeRecrords) 
            {
                workingHours = (TimeSpan) (attendanceRecord.DepartureTime) - (attendanceRecord.AttendanceTime);
                actualWorkingHours = workingHours.TotalHours;

                if(actualWorkingHours>workingHoursPerDay)
                {
                    bounsHours+=( actualWorkingHours - workingHoursPerDay);
                }
            }

            return bounsHours;
        }

        public async Task<double> CalcBounsHoursValue(double workingHours,double salary,double bounsHours)
        {
            //var employee = await _employeeService.GetByIdAsync(EmployeeId);
            var DeductionAddationRecord=await _generalSettingsService.GetDeducation_Addation();
            var HourValue = (salary / 30) / workingHours;
            var BounsValue = bounsHours * DeductionAddationRecord.AddationValue * HourValue;
            return BounsValue;

        }
        public async Task SaveChanges()
        {
            await _oldSalaryRepository.SaveAsync();
        }
    }
}
