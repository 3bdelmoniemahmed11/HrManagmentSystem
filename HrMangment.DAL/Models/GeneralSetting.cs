using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class GeneralSetting
    {
        public int Id { get; set; } 
        public DateOnly StartDate { get; set; } 
        public DateOnly EndDate { get; set; }   
        public int DeductionValue { get; set; } 
        public int AddationValue { get; set; }  
        //q1: we should add Isdeleted or not ?

    }
}
