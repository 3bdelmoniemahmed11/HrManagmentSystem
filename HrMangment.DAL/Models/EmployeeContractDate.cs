using HrManagment.DAL.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class EmployeeContractDate
    {
        public int Id { get; set; }

        [HireDate("1/1/2008")]

        public DateTime HireDate { get; set; }
        public DateTime FireDate { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }  
        public Employee Employee { get; set; }
    }
}
