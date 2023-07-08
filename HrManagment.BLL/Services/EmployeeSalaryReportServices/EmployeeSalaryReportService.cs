using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.BLL.Services.SalaryClickLogServices;
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
        public EmployeeSalaryReportService(
            IGenericRepository<OldSalary> oldSalaryRepository,
            IAttendanceService attendanceService,
             ISalaryClickLogService salaryClickService
            )
        {
            _oldSalaryRepository=oldSalaryRepository;
            _attendanceService = attendanceService;
            _salaryClickService = salaryClickService;


        }
        public async Task InsertAsyn(OldSalary oldSalary)
        {
            await _oldSalaryRepository.InsertAsync(oldSalary);
             await _oldSalaryRepository.SaveAsync();
        }

        public async Task<int> CalcAttendanceDays(int EmployeeId)
        {
            var startDate = await _salaryClickService.GetStartDate();
            var endDate = DateTime.Now;
            var NumberOfAttendanceDays = await _attendanceService.GetEmployeeAttendanceDays(EmployeeId, startDate.Date, endDate.Date);
            return NumberOfAttendanceDays.Count();
        }

        public async Task SaveChanges()
        {
            await _oldSalaryRepository.SaveAsync();
        }
    }
}
