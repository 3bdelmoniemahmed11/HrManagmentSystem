using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.AttendanceService
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Attendance> _attendanceRepository;
        public AttendanceService(IGenericRepository<Attendance> attendanceRepository)
        {
            _attendanceRepository= attendanceRepository;
        }
        public async Task<List<Attendance>> GetEmployeeAttendanceDays(int EmployeeId, DateTime startDate, DateTime endDate)
        {
            //var attendanceRecords = await _attendanceRepository.GetAllAsync();
            //var filteredRecords = attendanceRecords.Where(emp => emp.EmpId == EmployeeId
            //&& emp.Date.Date >= startDate && emp.Date.Date <= endDate);
            //return  await filteredRecords.ToListAsync();

            var attendanceRecords = await _attendanceRepository.GetFilteredAsync(emp => emp.EmpId == EmployeeId
            && emp.Date.Date >= startDate && emp.Date.Date <= endDate);
            return   attendanceRecords.ToList();

        }
    }
}
