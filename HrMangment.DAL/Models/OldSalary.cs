using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class OldSalary
    {
        public int Id { get; set; } 
        public float NetSalary { get; set; }
        public string DepartmentName { get; set; }
        public  int  BaseSalary { get; set; }
        public int AttendanceDays { get; set; }
        public int OffDays { get; set; }    
        public TimeSpan BounsHours { get; set; }
        public TimeSpan LateHours { get; set; }
        public float BounsHoursValue { get; set; }
        public float LateHoursValue { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }  
        public Employee Employee { get; set; }  
        
    }
}
