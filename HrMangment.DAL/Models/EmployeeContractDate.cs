using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class EmployeeContractDate
    {
        public int Id { get; set; } 
        public DateOnly HireDate { get; set; }
        public DateOnly FireDate { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }  
        public Employee Employee { get; set; }
    }
}
