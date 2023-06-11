﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.Models
{
    internal class Attendance
    {
        public int Id { get; set; } 
        public TimeSpan AttendanceTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public DateOnly Date { get; set; }  
        public bool IsDeleted { get; set; }
        [ForeignKey("EmpId")]
        public int EmpId { get; set; }  
        public Employee Employee { get; set; }
    }
}
