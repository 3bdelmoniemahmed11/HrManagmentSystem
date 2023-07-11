using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.EmployeeSalaryReport
{
    public interface IEmployeeSalaryReportService
    {
        Task InsertAsyn(OldSalary oldSalary);
        Task InsertListAsync(List<OldSalary> list);
        Task<int> CalcOffDays(int EmployeeId);
        Task<int> CalcAttendanceDays(int EmployeeId);
        Task<double> CalcBounsHours(int EmployeeId, TimeSpan DefaultAttendanceTime, TimeSpan DefaultDepartureTime);
        Task<double> CalcBounsHoursValue(double workingHours, double salary, double bounsHours);
        Task<double> CalcDeductionHours(int EmployeeId, TimeSpan DefaultAttendanceTime, TimeSpan DefaultDepartureTime);
        Task<double> CalcDeductionHoursValue(double workingHours, double salary, double DeductionHours);
        Task CalcSalariesForAllEmps();
        Task<IEnumerable<OldSalary>> GetAll();
        Task SaveChanges();
    }
}
