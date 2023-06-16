using HrManagment.DAL.CustomValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class Vacation
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        [CompanyDate("1/1/2008")]
        public DateTime Date { get; set; }  
        public string Type { get; set; }
        
    }
}
