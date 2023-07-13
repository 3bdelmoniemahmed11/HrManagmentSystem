using HrManagment.BLL.Services.AnnualVacationServices;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.DepartmentServices;
using HrManagment.BLL.Services.EmployeeContrActDataServices;
using HrManagment.BLL.Services.EmployeeSalaryReport;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.BLL.Services.GeneralSettingServices;
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
            RegisterWeeklyVacation(services);
            RegisterAnnualVacation(services);

            RegisterGeneralSetting(services);
            RegisterDepartment(services);
            RegisterEmployeeConteractData(services);

        }


        public static void RegisterGenericRepository(IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<OldSalary>, GenericRepository<OldSalary>>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            services.AddScoped<IGenericRepository<SalaryClickLog>, GenericRepository<SalaryClickLog>>();
            services.AddScoped<IGenericRepository<WeeklyVacation>, GenericRepository<WeeklyVacation>>();
            services.AddScoped<IGenericRepository<AnnualVacation>, GenericRepository<AnnualVacation>>();
            services.AddScoped<IGenericRepository<Attendance>, GenericRepository<Attendance>>();
            services.AddScoped<IGenericRepository<GeneralSetting>, GenericRepository<GeneralSetting>>();
            services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
            services.AddScoped<IGenericRepository<EmployeeContractDate>, GenericRepository<EmployeeContractDate>>();


        }
        public static void RegisterEmployeeSalaryReport(IServiceCollection services)
        {
            services.AddScoped<IEmployeeSalaryReportService, EmployeeSalaryReportService>();
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
            services.AddScoped<ISalaryClickLogService, SalaryClickLogService>();
        }
        public static void RegisterWeeklyVacation(IServiceCollection services)
        {
            services.AddScoped<IWeeklyVacationService, WeeklyVacationService>();
        }
        public static void RegisterAnnualVacation(IServiceCollection services)
        {
            services.AddScoped<IAnnualVacationService, AnnualVacationService>();
        }
        public static void RegisterGeneralSetting(IServiceCollection services)
        {
            services.AddScoped<IGeneralSettingsService, GeneralSettingsService>();
        }
        public static void RegisterEmployeeConteractData(IServiceCollection services)
        {
            services.AddScoped<IEmployeeContrActDataService, EmployeeContrActDataService>();
        }
        public static void RegisterDepartment(IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
        }


    }
}
