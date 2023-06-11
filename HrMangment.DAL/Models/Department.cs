using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public bool IsDeleted { get; set; } 
        List<Employee> Employees { get; set; }=new List<Employee>();
    }
}
