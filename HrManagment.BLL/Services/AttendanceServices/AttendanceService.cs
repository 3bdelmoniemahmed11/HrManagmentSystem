using AutoMapper;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Models.DTOs;
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
        private readonly IMapper _mapper;
        public AttendanceService(IGenericRepository<Attendance> attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }
        public async Task<List<Attendance>> GetEmployeeAttendanceDays(int EmployeeId, DateTime startDate, DateTime endDate)
        {

            var attendanceRecords = await _attendanceRepository.GetFilteredAsync(emp => emp.EmpId == EmployeeId
            && emp.Date.Date >= startDate && emp.Date.Date <= endDate && emp.IsDeleted == false);
            return attendanceRecords.ToList();

        }

        public async Task<List<EmpAttendanceDTO>> GetAllEmpsAttendances()
        {

            var attendanceRecords = await _attendanceRepository.GetFilteredIncluded(att => att.IsDeleted == false, "Employee.Department");
            List<EmpAttendanceDTO> empAttDTO = new List<EmpAttendanceDTO>();
            empAttDTO = _mapper.Map<List<EmpAttendanceDTO>>(attendanceRecords);
            return empAttDTO;

        }

        public async Task Delete(int id)
        {
            
            var empAtt = await _attendanceRepository.GetByIdAsync(id);
            empAtt.IsDeleted = true;
            await _attendanceRepository.SaveAsync();
        }

        public async Task AddAttAsync(Attendance attendance)
        {
            await _attendanceRepository.InsertAsync(attendance);
            await _attendanceRepository.SaveAsync();
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
           return await _attendanceRepository.GetByIdAsync(id);
        
        }

        public async Task Update(Attendance attendance)
        {
           await _attendanceRepository.Update(attendance);
           await _attendanceRepository.SaveAsync();
        }

        public async Task<Attendance> GetByIdAsynAsNoTracking(int id)
        {
            return await _attendanceRepository.GetByIdAsynAsNoTracking(id);
        }

        public async Task InsertListAsync(List<Attendance> list)
        {
            await _attendanceRepository.InsertListAsync(list);
            await _attendanceRepository.SaveAsync();
        }
    }
}
