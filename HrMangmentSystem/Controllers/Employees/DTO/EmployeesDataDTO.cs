using HrManagment.DAL.CustomValidation;
using HrManagment.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HrManagmentSystem.Controllers.EmployeesPersonalData.DTO
{
    public class EmployeesDataDTO
    {
        public int Id { get; set; }
        [MaxLength]
        public string Name { get; set; }
        //[RegularExpression("^[0-9]+{14,14}$", ErrorMessage = "Please enter a valid SSN")]
        [MinLength(14)]
        public string SSN { get; set; }
        public string Address { get; set; }

        [RegularExpression("^[0-9]{11,}$", ErrorMessage = "Please enter a valid Phone number.")]
        [MinLength(11)]
        public string Phone { get; set; }
        public string Gender { get; set; }
        //check the age is greater than 20 by two ways 1:custom validation  
        //[Age(20)]
        public string BirthDate { get; set; }
        public string HirDate { get; set; }
        public string Nationality { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter a valid Salary")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public int NetSalary { get; set; }
        public string AttendanceTime { get; set; }
        //[TimeSpanComparison("AttendanceTime")]
        public string DepartureTime { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeptId { get; set; }
        public string Department { get; set; }
    }
}
