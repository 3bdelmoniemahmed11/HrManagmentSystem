using HrManagment.DAL.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models.DTOs
{
    public class EmpAttendanceDTO
    {
        public int Id { get; set; }
        public TimeSpan AttendanceTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public DateTime Date { get; set; }
        public string? EmpName { get; set; }
        public string? DeptName { get; set; }   
        public int EmpId { get; set; }


    }
}
