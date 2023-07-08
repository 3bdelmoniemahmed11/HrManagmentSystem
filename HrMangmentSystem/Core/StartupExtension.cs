using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.EmployeeSalaryReport;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.BLL.Services.SalaryClickLogServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System.Runtime.CompilerServices;

namespace HrManagmentSystem.Core
{
    public static class StartupExtension
    {
    
        public static void RegisterServices(this IServiceCollection services)
        {
            RegisterEmployeeSalaryReport(services);
            RegisterGenericRepository(services);
            RegisterAttendance(services);
            RegisterEmployee(services);
            RegisterSalaryClickLog(services);
            RegisterVacation(services);

         }
        public static void RegisterEmployeeSalaryReport(IServiceCollection services)
        {
            services.AddScoped<IEmployeeSalaryReportService, EmployeeSalaryReportService>();
        }

        public static void RegisterGenericRepository(IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<OldSalary>, GenericRepository<OldSalary>>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            services.AddScoped<IGenericRepository<SalaryClickLog>, GenericRepository<SalaryClickLog>>();
            services.AddScoped<IGenericRepository<Vacation>, GenericRepository<Vacation>>();
        }

        public static void RegisterAttendance(IServiceCollection services)
        {
            services.AddScoped<IAttendanceService, AttendanceService>();
        }

        public static void RegisterEmployee(IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        public static void RegisterSalaryClickLog(IServiceCollection services)
        {
            services.AddScoped<ISalaryClickLogService,SalaryClickLogService>();
        }
        public static void RegisterVacation(IServiceCollection services)
        {
            services.AddScoped<IVacationService, VacationService>();
        }


    }
}
