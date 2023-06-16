using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class Department
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public bool ?IsDeleted { get; set; } 
       public virtual List<Employee> Employees { get; set; }=new List<Employee>();
    }
}
