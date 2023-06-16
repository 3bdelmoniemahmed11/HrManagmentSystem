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
    public class Employee
    {


        public int Id { get; set; }
        [MaxLength]
        public string Name { get; set; }
        [RegularExpression("^[0-9]+{14,14}$", ErrorMessage = "Please enter a valid SSN")]
        [MinLength(14)]
        public string SSN { get; set; }
        public string Address { get; set; }

        [RegularExpression("^[0-9]{11,}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(11)]
        public string Phone { get; set; }   
        public string Gender { get; set; }
        //check the age is greater than 20 by two ways 1:custom validation  
        [Age(20)]
        public DateTime BirthDate { get; set; } 
        public string Nationality { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter a valid Salary")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public int NetSalary { get; set; }  
        public TimeSpan AttendanceTime { get; set; }
        [TimeSpanComparison("AttendanceTime")]
        public TimeSpan DepartureTime { get; set; }
        public bool ?IsDeleted { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; } 
        public Department Department { get; set; }

       public  virtual List<Attendance> Attendances { get; set; }= new List<Attendance>();
       public virtual List<EmployeeContractDate> ContractsDates { get; set; } = new List<EmployeeContractDate>();
       public virtual List<OldSalary>   OldSalarys { get; set;} = new List<OldSalary>();    
    }
}
