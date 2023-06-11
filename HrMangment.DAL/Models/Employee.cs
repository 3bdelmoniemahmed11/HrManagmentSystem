using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class Employee
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }   
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; } 
        public string Nationality { get; set; } 
        public int NetSalary { get; set; }  
        public TimeSpan AttendanceTime { get; set; }    
        public TimeSpan DepartureTime { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; } 
        public Department Department { get; set; }

       public  virtual List<Attendance> Attendances { get; set; }= new List<Attendance>();
       public virtual List<EmployeeContractDate> ContractsDates { get; set; } = new List<EmployeeContractDate>();
       public virtual List<OldSalary>   OldSalarys { get; set;} = new List<OldSalary>();    
    }
}
