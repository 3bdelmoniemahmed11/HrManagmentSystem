using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.Models
{
    public class AnnualVacation
    {
        public int Id { get; set; }
        public string VcationName { get; set; }
        public DateTime VacationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
