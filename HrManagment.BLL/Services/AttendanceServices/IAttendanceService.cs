using HrManagment.DAL.Models;
using HrManagment.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.AttendanceService
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetEmployeeAttendanceDays(int EmployeeId, DateTime startDate, DateTime endDate);
        Task<List<EmpAttendanceDTO>> GetAllEmpsAttendances();
        Task Delete(int id);
        Task AddAttAsync(Attendance attendance);

        Task<Attendance> GetByIdAsync(int id);
        Task Update(Attendance attendance);
        Task<Attendance> GetByIdAsynAsNoTracking(int id);
        Task InsertListAsync(List<Attendance> list);



    }
}
