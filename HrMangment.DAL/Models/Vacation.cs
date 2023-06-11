using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class Vacation
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public DateOnly Date { get; set; }  
        public string Type { get; set; }
        //q1: we should add Isdeleted or not ?
    }
}
