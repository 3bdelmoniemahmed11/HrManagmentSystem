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
        public DateTime Date { get; set; }  
        public string Type { get; set; }
        //q1: we should add Isdeleted or not ?
    }
}
