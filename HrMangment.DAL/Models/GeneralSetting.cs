using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class GeneralSetting
    {
        public int Id { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }   
        public int DeductionValue { get; set; } 
        public int AddationValue { get; set; }  
        

    }
}
