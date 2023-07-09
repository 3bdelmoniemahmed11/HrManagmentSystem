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
        Task<int> CalcOffDays(int EmployeeId);
        Task<int> CalcAttendanceDays(int EmployeeId);
        Task SaveChanges();
    }
}
