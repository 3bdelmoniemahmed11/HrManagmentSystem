using HrManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.DBContext
{
    public class HrManagmentContext: DbContext
    {
        public HrManagmentContext(DbContextOptions<HrManagmentContext> options) : base(options) { }

        DbSet<Employee > Employee { get; set; } 
        DbSet<Department> Department { get; set; }  
        DbSet<Attendance> Attendance { get; set; }  
        DbSet<EmployeeContractDate> EmployeeContractDate { get; set; }  
        DbSet<GeneralSetting> GeneralSettings { get; set; }
        DbSet<OldSalary> OldSalary { get; set; }    
        DbSet<Vacation> Vacation { get; set; }  
        


    }
}
