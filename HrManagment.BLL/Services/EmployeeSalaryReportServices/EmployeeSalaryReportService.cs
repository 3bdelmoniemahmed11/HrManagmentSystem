using HrManagment.BLL.Services.AnnualVacationServices;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.BLL.Services.GeneralSettingsServices;
using HrManagment.BLL.Services.SalaryClickLogServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
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
            var requiredWorkDays = await CalcRequiredWorkDays();
            var numAttendanceDays = await CalcAttendanceDays(EmployeeId);
            var offDays = requiredWorkDays - numAttendanceDays;
            return offDays;
        }
        public async Task<int> CalcRequiredWorkDays()
        {
            var startDate = await _salaryClickService.GetStartDate();
            var endDate = DateTime.Now;

            var numOfweeklyVacationDays = await _weeklyVacationService.GetNumberOfWeeklyVacationsByPeriod(startDate, endDate);
            var annualVacationDays = await _annualVacationService.GetAnnualVacationsByPeriod(startDate, endDate);
            var numOfAnnualVacationDays = annualVacationDays.Count();
            var requiredWorkDays = (endDate.Date - startDate.Date).Days - (numOfweeklyVacationDays + numOfAnnualVacationDays);
            return requiredWorkDays;
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
            var DeductionAddationRecord = await _generalSettingsService.GetDeducation_Addation();
            var requiredWorkDays = await CalcRequiredWorkDays();
            var HourValue = (salary / requiredWorkDays) / workingHours;
            var BounsValue = bounsHours * DeductionAddationRecord.AddationValue * HourValue;
            return BounsValue;

        }
        public async Task<double> CalcDeductionHours(int EmployeeId, TimeSpan DefaultAttendanceTime, TimeSpan DefaultDepartureTime)
        {
            var deductionHours = 0.00;
            var actualWorkingHours = 0.0;
            var startDate = await _salaryClickService.GetStartDate();
            var endDate = DateTime.Now;

            var attendanceEmployeeRecrords = await _attendanceService.GetEmployeeAttendanceDays(EmployeeId, startDate.Date, endDate.Date);
            var workingHoursPerDay = (DefaultDepartureTime - DefaultAttendanceTime).TotalHours;
            TimeSpan workingHours;
            foreach (var attendanceRecord in attendanceEmployeeRecrords)
            {
                workingHours = (TimeSpan)(attendanceRecord.DepartureTime) - (attendanceRecord.AttendanceTime);
                actualWorkingHours = workingHours.TotalHours;

                if (actualWorkingHours < workingHoursPerDay)
                {
                    deductionHours += (workingHoursPerDay - actualWorkingHours);
                }
            }

            return deductionHours;
        }

        public async Task<double> CalcDeductionHoursValue(double workingHours, double salary, double DeductionHours)
        {
            //var employee = await _employeeService.GetByIdAsync(EmployeeId);
            var DeductionAddationRecord = await _generalSettingsService.GetDeducation_Addation();
            var requiredWorkDays = await CalcRequiredWorkDays();
            var HourValue = (salary / requiredWorkDays) / workingHours;
            var DeductionValue = DeductionHours * DeductionAddationRecord.DeductionValue * HourValue;
            return DeductionValue;
        }
        public async Task<double> CalcDeductionValueForAbsenceDays(double workingHours, double salary, int EmployeeId)
        {
            var empAbsenceDays = await CalcOffDays(EmployeeId);
            var requiredWorkDays = await CalcRequiredWorkDays();

            var empWorkingHourValue = (salary / requiredWorkDays) / workingHours;
            var empAbsenceDeductionValue = empAbsenceDays * workingHours * empWorkingHourValue;
            return empAbsenceDeductionValue;
        }
        public async Task CalcSalariesForAllEmps()
        {
            var allEmps = await _employeeService.GetAllEmpsIncludingDeptAsync();
            List<OldSalary> oldSalaries = new List<OldSalary>();
            foreach (var emp in allEmps)
            {
                var empBonusHours = await CalcBounsHours(emp.Id, emp.AttendanceTime, emp.DepartureTime);
                var empBonusHoursValue = await CalcBounsHoursValue((emp.DepartureTime - emp.AttendanceTime).TotalHours, emp.NetSalary, empBonusHours);

                var empDeductionHours = await CalcDeductionHours(emp.Id, emp.AttendanceTime, emp.DepartureTime);
                var empDeductionHoursValue = await CalcDeductionHoursValue((emp.DepartureTime - emp.AttendanceTime).TotalHours, emp.NetSalary, empDeductionHours);

                var DeductionValueForAbsenceDays = await CalcDeductionValueForAbsenceDays((emp.DepartureTime - emp.AttendanceTime).TotalHours ,emp.NetSalary,emp.Id);

                var finalNetSalary = emp.NetSalary + empBonusHoursValue - empDeductionHoursValue - DeductionValueForAbsenceDays;

                OldSalary oldSal = new OldSalary()
                {
                    BaseSalary = emp.NetSalary,
                    NetSalary =  Math.Round(finalNetSalary, 3),
                    DepartmentName = emp.Department.Name,
                    AttendanceDays = await CalcAttendanceDays(emp.Id),
                    OffDays = await CalcOffDays(emp.Id),
                    BounsHours = Math.Round(empBonusHours,3),
                    LateHours = Math.Round(empDeductionHours,3),
                    BounsHoursValue = Math.Round(empBonusHoursValue,3),
                    LateHoursValue = Math.Round(empDeductionHoursValue,3),
                    Date = DateTime.Now,
                    EmpId = emp.Id

                };
                oldSalaries.Add(oldSal);
            }
            await InsertListAsync(oldSalaries);
            await SaveChanges();
            
        }

        public async Task SaveChanges()
        {
            await _oldSalaryRepository.SaveAsync();
        }

        public async Task InsertListAsync(List<OldSalary> list)
        {
            await _oldSalaryRepository.InsertListAsync(list);
        }

        public async Task<IEnumerable<OldSalary>> GetAll() 
        {
            var res = await  _oldSalaryRepository.GetAllAsync();
            return  res;
        }
    }
}
