using HrManagment.BLL.Services.AnnualVacationServices;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.EmployeeServices;
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

        public EmployeeSalaryReportService
        (   
            IGenericRepository<OldSalary> oldSalaryRepository,
            IAttendanceService attendanceService,
            ISalaryClickLogService salaryClickService,
            IWeeklyVacationService weeklyVacationService,
            IAnnualVacationService annualVacationService
        )
        {
            _oldSalaryRepository = oldSalaryRepository;
            _attendanceService = attendanceService;
            _salaryClickService = salaryClickService;
            _weeklyVacationService = weeklyVacationService;
            _annualVacationService = annualVacationService;
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

        public async Task SaveChanges()
        {
            await _oldSalaryRepository.SaveAsync();
        }
    }
}
