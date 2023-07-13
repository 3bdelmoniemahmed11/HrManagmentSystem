using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class OldSalary
    {
        public int Id { get; set; } 
        public double NetSalary { get; set; } 
        public string DepartmentName { get; set; } 
        public  double  BaseSalary { get; set; } 
        public int AttendanceDays { get; set; }
        public int OffDays { get; set; }    
        public double BounsHours { get; set; }
        public double LateHours { get; set; }
        public double BounsHoursValue { get; set; }
        public double LateHoursValue { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }  
        public Employee Employee { get; set; }  
        
    }
}
