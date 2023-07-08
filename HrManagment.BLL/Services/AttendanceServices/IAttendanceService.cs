using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.AttendanceService
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetEmployeeAttendanceDays(int EmployeeId,DateTime startDate ,DateTime endDate);
    }
}
