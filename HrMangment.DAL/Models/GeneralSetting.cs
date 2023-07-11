using HrManagment.DAL.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class GeneralSetting
    {
        public int Id { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public int DeductionValue { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public int AddationValue { get; set; }  
        

    }
}
