using HrManagment.DAL.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class Attendance
    {
        public int Id { get; set; } 
        public TimeSpan AttendanceTime { get; set; }

        [TimeSpanComparison("AttendanceTime")]
        public TimeSpan ?DepartureTime { get; set; }
        
        public DateTime Date { get; set; }
        public bool? IsDeleted { get; set; } = false;

        [ForeignKey("Employee")]
        public int EmpId { get; set; }  
        public Employee? Employee { get; set; }
    }
}
